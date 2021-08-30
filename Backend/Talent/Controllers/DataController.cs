using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Talent.Services.Interfaces;

namespace Talent.Controllers
{
    [ApiController]
    [Route("data/")]
    public class DataController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DataController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("connectsql")]
        public IActionResult CreateDatasetFromSql([FromBody] string connectionString, [FromBody] string tableName)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch
            {
                return BadRequest("Cannot connect to the sql.");
            }
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("sql-table-list")]
        public ActionResult<List<string>> GetListOfTables([FromBody] string connectionString)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                var schema = connection.GetSchema("Tables");
                return (from DataRow row in schema.Rows select row[2].ToString()).ToList();
            }
            catch
            {
                return BadRequest("Cannot connect to the sql.");
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