using System;
using Microsoft.AspNetCore.Mvc;
using Talent.Models;

namespace Talent.Controllers
{
    [ApiController]
    [Route("data/")]
    public class DataController
    {
        [HttpPost]
        [Route("connectsql")]
        public IActionResult ConnectToSql([FromBody] ConnectionString connectionString)
        {
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