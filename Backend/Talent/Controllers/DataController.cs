using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Talent.Data.Entities;
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
                OpenConnection(_serverConnection);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Cannot connect to the server database.");
            }
        }

        private void OpenConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        [HttpPost]
        [Route("connectsql")]
        public IActionResult CreateDatasetFromSql([FromBody] TableConnection tableConnection)
        {
            try
            {
                using var connection = new SqlConnection(tableConnection.ConnectionString.ToString());
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var newDataSource = _sqlParser.CloneTable(connection,
                    _serverConnection, tableConnection.SourceTable, tableConnection.DestTable);
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
        public ActionResult GetListOfTables([FromBody] ConnectionString connectionString)
        {
            using var connection = new SqlConnection(connectionString.ToString());
            try
            {
                OpenConnection(connection);
                var schema = connection.GetSchema("Tables");
                return Ok(Json((from DataRow row in schema.Rows select row[2].ToString()).ToArray()));
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