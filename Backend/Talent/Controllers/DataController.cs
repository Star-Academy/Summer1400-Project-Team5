using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Talent.Services.Interfaces;

namespace Talent.Controllers
{
    [ApiController]
    [Route("data/")]
    public class DataController : Controller
    {
        private IUnitOfWork _unitOfWork;

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
                return BadRequest("Cannot connect to sql.");
            }

            throw new NotImplementedException();
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

        [HttpDelete]
        [Route("sql/delete/{id:int}")]
        public IActionResult DeleteTable(int id)
        {
            throw new NotImplementedException();
        }
    }
}