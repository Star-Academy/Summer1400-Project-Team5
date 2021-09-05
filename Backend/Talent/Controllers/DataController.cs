using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
        private readonly SqlConnection _serverConnection;
        private readonly ISqlParser _sqlParser;
        private readonly ICsvParser _csvParser;
        private readonly ICsvDownloader _csvDownloader;

        public DataController(IUnitOfWork unitOfWork, SqlConnection serverConnection, ISqlParser sqlParser, ICsvParser csvParser, ICsvDownloader csvDownloader)
        {
            _unitOfWork = unitOfWork;
            _serverConnection = serverConnection;
            _sqlParser = sqlParser;
            _csvParser = csvParser;
            _csvDownloader = csvDownloader;
            try
            {
                _serverConnection.Open();
            }
            catch
            {
                throw new Exception("Cannot connect to the server database.");
            }
        }

        [HttpPost]
        [Route("connectsql")]
        public IActionResult CreateDatasetFromSql([FromBody] TableConnection tableConnection)
        {
            try
            {
                using var connection = new SqlConnection(tableConnection.connectionString.ToString());
                connection.Open();
                var newDataSource = _sqlParser.CloneTable(connection,
                    _serverConnection, tableConnection.tableName, tableConnection.tableName + "CLONED"); // destName can be changed
                _unitOfWork.DataSources.Insert(newDataSource);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Cannot connect to the database.");
            }
        }

        [HttpPost]
        [Route("uploadCsv")]
        public IActionResult CreateDatabaseFromCsv([FromBody] CsvConnection csvConnection)
        {
            try
            {
                var newDataSource = _csvParser.ConvertCsvToSql(_serverConnection, csvConnection.TableName, csvConnection.CsvFile);
                // _unitOfWork.DataSources.Insert(newDataSource);
                return Ok();
            }
            catch
            {
                return BadRequest("Cannot connect to the database.");
            }
        }

        [HttpGet]
        [Route("sql-table-list")]
        public ActionResult<List<string>> GetListOfTables([FromBody] ConnectionString connectionString)
        {
            try
            {
                using var connection = new SqlConnection(connectionString.ToString());
                connection.Open();
                var schema = connection.GetSchema("Tables");
                return (from DataRow row in schema.Rows select row[2].ToString()).ToList();
            }
            catch
            {
                return BadRequest("Cannot connect to the database.");
            }
        }

        [HttpGet]
        [Route("sql")]
        public IActionResult GetSqlList()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("sql/{id:int}")]
        public IActionResult GetTablePreview(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("csv/download")]
        public IActionResult DownloadCsv(CsvConnection csvConnection)
        {
            try
            {
                var datasource = _unitOfWork.DataSources.Get(d => d.TableName == csvConnection.TableName);
                var fileContent = _csvDownloader.DownloadCsv(datasource.Result.SqlConnection, csvConnection.TableName, csvConnection.CsvFile);
                return File(Encoding.ASCII.GetBytes(fileContent), "text/csv", "data.csv");
            }
            catch
            {
                return BadRequest("Download failed. Cannot connect to the database.");
            }
        }

        [HttpDelete]
        [Route("sql/delete/{id:int}")]
        public IActionResult DeleteSql(int id, string tableName)
        {
            var datasource = _unitOfWork.DataSources.Get(d => d.TableName == tableName);
            datasource.Result.DropTable();
            _unitOfWork.DataSources.Delete(id);
            return Ok();
        }
    }
}