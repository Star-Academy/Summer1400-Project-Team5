using System;
using Microsoft.AspNetCore.Mvc;
using Talent.Models;
using Talent.Services.Interfaces;
using Talent.Services.Repositories;

namespace Talent.Controllers
{
    [ApiController]
    [Route("data/")]
    public class DataController
    {
        private IUnitOfWork _unitOfWork;

        public DataController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("connectsql")]
        public IActionResult ConnectToSql([FromBody] ConnectionString connectionString, [FromBody] string tableName)
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