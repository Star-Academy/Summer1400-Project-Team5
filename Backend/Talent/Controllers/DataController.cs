using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Talent.Models;
using Talent.Models.DatabaseModels;
using Talent.Services.Interfaces;

namespace Talent.Controllers
{
    [ApiController]
    [Route("data/")]
    public class DataController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISqlHandler _sqlHandler;
        private readonly ISqlParser _sqlParser;
        private readonly ICsvParser _csvParser;
        private readonly ICsvDownloader _csvDownloader;
        private readonly ISqlToJson _sqlToJson;

        public DataController(IUnitOfWork unitOfWork, ISqlHandler sqlHandler, ISqlParser sqlParser, ICsvParser csvParser, ICsvDownloader csvDownloader, ISqlToJson sqlToJson)
        {
            _unitOfWork = unitOfWork;
            _sqlHandler = sqlHandler;
            _sqlParser = sqlParser;
            _csvParser = csvParser;
            _csvDownloader = csvDownloader;
            _sqlToJson = sqlToJson;
        }

        private void CloseConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        [HttpPost]
        [Route("connectSql")]
        public async Task<IActionResult> CreateDatasetFromSql([FromBody] TableConnection tableConnection)
        {
            await using var connection = new SqlConnection(tableConnection.ConnectionString.ToString());
            try
            {
                CloseConnection(connection);
                connection.Open();
                var newDataSource = _sqlParser.CloneTable(connection.Database,
                    _sqlHandler.Connection.Database, tableConnection.SourceTable, tableConnection.DestTable);
                await _unitOfWork.DataSources.Insert(newDataSource);
                await _unitOfWork.Save();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Cannot connect to the database.");
            }
            finally
            {
                CloseConnection(connection);
            }
        }

        [HttpPost]
        [Route("uploadCsv")]
        public async Task<IActionResult> CreateDatabaseFromCsv([FromQuery] string delimiter, [FromQuery] bool hasHeader, [FromQuery] string tableName)
        {
            try
            {
                var file = Request.Form.Files[0];
                var csvFile = new CsvFile()
                {
                    FormFile = file,
                    Delimiter = delimiter,
                    HasHeader = hasHeader
                };
                var newDataSource = _csvParser.ConvertCsvToSql(tableName, csvFile);
                await _unitOfWork.DataSources.Insert(newDataSource);
                await _unitOfWork.Save();
                return Ok("upload successful.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Cannot connect to the database.");
            }
        }

        [HttpPost]
        [Route("sql-table-list")]
        public ActionResult GetListOfTablesInDb([FromBody] ConnectionString connectionString)
        {
            using var connection = new SqlConnection(connectionString.ToString());
            try
            {
                CloseConnection(connection);
                connection.Open();
                var schema = connection.GetSchema("Tables");
                var result = (from DataRow row in schema.Rows select row[2].ToString()).ToList();
                return Ok(Json(result));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Cannot connect to the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        [HttpGet]
        [Route("dataSource-list")]
        public OkObjectResult GetListOfDataSources()
        {
            var dataSources = _unitOfWork.DataSources.GetAll().Result;
            return Ok(dataSources);
        }

        [HttpGet]
        [Route("tempDatasource-list")]
        public OkObjectResult GetListOfTempDataSources()
        {
            var dataSources = _unitOfWork.TempDataSources.GetAll().Result;
            return Ok(dataSources);
        }

        [HttpGet]
        [Route("sql/dataSource/{id:int}")]
        public IActionResult GetDataSourceTablePreview(int id, [FromQuery] int rowCount)
        {
            try
            {
                var dataSource = _unitOfWork.DataSources.Get(d => d.Id == id).Result; 
                return Ok(rowCount == 0 ? _sqlToJson.ConvertSqlTableToJson(dataSource.TableName) : _sqlToJson.ConvertSqlTableToJson(dataSource.TableName, rowCount));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("error occurred.");
            }
        }

        [HttpGet]
        [Route("sql/tempDataSource/{id:int}")]
        public IActionResult GetTempTablePreview(int id, [FromQuery] int rowCount)
        {
            try
            {
                var dataSource = _unitOfWork.TempDataSources.Get(d => d.Id == id).Result; 
                return Ok(rowCount == 0 ? _sqlToJson.ConvertSqlTableToJson(dataSource.TableName) : _sqlToJson.ConvertSqlTableToJson(dataSource.TableName, rowCount));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("error occurred.");
            }
            
        }

        [HttpGet]
        [Route("csv/download")]
        public IActionResult DownloadCsv([FromBody] CsvConnection csvConnection)
        {
            try
            {
                var fileContent = _csvDownloader.DownloadCsv(csvConnection.TableName, csvConnection.CsvFile);
                return File(Encoding.ASCII.GetBytes(fileContent), "text/csv", $"{csvConnection.TableName}.csv");
            }
            catch
            {
                return BadRequest("Download failed.");
            }
        }

        [HttpDelete]
        [Route("sql/delete/{id:int}")]
        public async Task<IActionResult> DeleteSql(int id)
        {
            var datasource = _unitOfWork.DataSources.Get(d => d.Id == id);
            _sqlHandler.DropTableIfExists(datasource.Result.TableName);
            await _unitOfWork.DataSources.Delete(id);
            await _unitOfWork.Save();
            return Ok();
        }
    }
}