using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Talent.Data.Entities;
using Talent.Models;
using Talent.Services.Interfaces;

namespace Talent.Controllers
{
    [ApiController]
    [Route("data/")]
    public class DataController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqlConnection _serverConnection;

        public DataController(IUnitOfWork unitOfWork, SqlConnection serverConnection)
        {
            _unitOfWork = unitOfWork;
            _serverConnection = serverConnection;
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
        public IActionResult CreateDatasetFromSql([FromBody] ConnectionString connectionString, [FromBody] string tableName)
        {
            try
            {
                using var connection = new SqlConnection(connectionString.ToString());
                connection.Open();
                var newTable = new SqlTable(_serverConnection, tableName); //TODO tableName can be changed to a custom name
                newTable.LoadTableFromExistingOne(connection, tableName);
                _unitOfWork.DataSources.Insert(new DataSource(newTable));
                return Ok();
            }
            catch
            {
                return BadRequest("Cannot connect to the database.");
            }
        }

        [HttpPost]
        [Route("uploadcsv")]
        public IActionResult CreateDatabaseFromCsv([FromBody] IFormFile csvFile, [FromBody] bool hasHeader, [FromBody] char separator)
        {
            throw new NotImplementedException();
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
        [Route("csv/download/{id:int}")]
        public IActionResult DownloadCsv(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("sql/delete/{id:int}")]
        public IActionResult DeleteSql(int id)
        {
            _unitOfWork.DataSources.Delete(id);
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("csv/delete/{id:int}")]
        public IActionResult DeleteCsv(int id)
        {
            _unitOfWork.DataSources.Delete(id);
            throw new NotImplementedException();
        }
    }
}