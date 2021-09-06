using System;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Talent.Data.Entities;
using Talent.Models;
using Talent.Models.DatabaseModels;
using Talent.Services.Interfaces;
using Talent.Services.Repositories;

namespace Talent.Controllers
{
    [ApiController]
    [Route("data/")]
    public class DataController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqlHandler _sqlHandler;
        private readonly ISqlParser _sqlParser;
        private readonly ICsvParser _csvParser;
        private readonly ICsvDownloader _csvDownloader;

        public DataController(IUnitOfWork unitOfWork, SqlHandler sqlHandler, ISqlParser sqlParser, ICsvParser csvParser, ICsvDownloader csvDownloader)
        {
            _unitOfWork = unitOfWork;
            _sqlHandler = sqlHandler;
            _sqlParser = sqlParser;
            _csvParser = csvParser;
            _csvDownloader = csvDownloader;
        }

        private void CloseConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        [HttpPost]
        [Route("connectsql")]
        public IActionResult CreateDatasetFromSql([FromBody] TableConnection tableConnection)
        {
            using var connection = new SqlConnection(tableConnection.ConnectionString.ToString());
            try
            {
                CloseConnection(connection);
                connection.Open();
                var newDataSource = _sqlParser.CloneTable(connection.Database,
                    _sqlHandler.Connection.Database, tableConnection.SourceTable, tableConnection.DestTable);
                _unitOfWork.DataSources.Insert(newDataSource);
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
        public IActionResult CreateDatabaseFromCsv([FromQuery] string delimiter, [FromQuery] bool hasHeader, [FromQuery] string tableName)
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
                _unitOfWork.DataSources.Insert(newDataSource);
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
        public ActionResult GetListOfTables([FromBody] ConnectionString connectionString)
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
        [Route("sql")]
        public IActionResult GetSqlList()
        {
            _unitOfWork.DataSources.Insert(new DataSource("Scores", "blah blah"));
            _unitOfWork.Save();
            var dataSources = _unitOfWork.DataSources.GetAll().Result;
            Console.WriteLine(dataSources.Count);
            return Ok(dataSources);
        }

        [HttpGet]
        [Route("sql/{id:int}")]
        public IActionResult GetTablePreview(int id)
        {
            throw new NotImplementedException();
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
        public IActionResult DeleteSql(int id)
        {
            var datasource = _unitOfWork.DataSources.Get(d => d.Id == id);
            _sqlHandler.DropTableIfExists(datasource.Result.TableName);
            _unitOfWork.DataSources.Delete(id);
            return Ok();
        }
    }
}