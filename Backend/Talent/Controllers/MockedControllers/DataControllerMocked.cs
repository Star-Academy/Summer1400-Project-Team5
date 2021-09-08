using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Talent.Models;
using Talent.Models.DatabaseModels;

namespace Talent.Controllers.MockedControllers
{
    [ApiController]
    [Route("mock/data/")]
    public class DataControllerMocked : Controller
    {
        [HttpPost]
        [Route("connectSql")]
        public IActionResult CreateDatasetFromSql([FromBody] TableConnection tableConnection)
        {
            return Ok("Database created successfully.");
        }

        [HttpPost]
        [Route("uploadCsv")]
        public IActionResult CreateDatabaseFromCsv([FromQuery] string delimiter, [FromQuery] bool hasHeader, [FromQuery] string tableName)
        {
            return Ok("Database created successfully.");
        }

        [HttpPost]
        [Route("sql-table-list")]
        public ActionResult GetListOfTablesInDb([FromBody] ConnectionString connectionString)
        {
            return Ok("{\n    \"contentType\": null,\n    \"serializerSettings\": null,\n    \"statusCode\": null,\n    \"value\": [\n        \"Mammad\",\n        \"MammadCLONED\",\n        \"Documents\",\n        \"Words\",\n        \"WordDocuments\"\n    ]\n}\n");
        }

        [HttpGet]
        [Route("dataSource-list")]
        public OkObjectResult GetListOfDataSources()
        {
            return Ok("[\n    {\n        \"id\": 1,\n        \"tableName\": \"Talend\",\n        \"databaseName\": \"blah blah\"\n    },\n    {\n        \"id\": 2,\n        \"tableName\": \"TalenT\",\n        \"databaseName\": \"blah blah\"\n    }\n]\n");
        }

        [HttpGet]
        [Route("tempDatasource-list")]
        public OkObjectResult GetListOfTempDataSources()
        {
            return Ok("[\n    {\n        \"id\": 5,\n        \"tableName\": \"MammadCLONED\",\n        \"databaseName\": \"Talend\"\n    },\n    {\n        \"id\": 6,\n        \"tableName\": \"MammadCLONED\",\n        \"databaseName\": \"Talend\"\n    }\n]\n");
        }

        [HttpGet]
        [Route("sql/dataSource/{id:int}")]
        public IActionResult GetDataSourceTablePreview(int id, [FromQuery] int rowCount)
        {
            return Ok(
                "[\n    {\n        \"Id\": 2,\n        \"TableName\": \"TalenT\",\n        \"AppUserId\": null,\n        \"DatabaseName\": \"blah blah\"\n    },\n    {\n        \"Id\": 1002,\n        \"TableName\": \"Mammad\",\n        \"AppUserId\": null,\n        \"DatabaseName\": \"Talend\"\n    }\n]\n");
        }

        [HttpGet]
        [Route("sql/tempDataSource/{id:int}")]
        public IActionResult GetTempTablePreview(int id, [FromQuery] int rowCount)
        {
            return Ok("[\n    {\n        \"Id\": 2,\n        \"TableName\": \"TalenT\",\n        \"AppUserId\": null,\n        \"DatabaseName\": \"blah blah\"\n    },\n    {\n        \"Id\": 1002,\n        \"TableName\": \"Mammad\",\n        \"AppUserId\": null,\n        \"DatabaseName\": \"Talend\"\n    }\n]\n");
        }

        [HttpGet]
        [Route("csv/download")]
        public IActionResult DownloadCsv([FromBody] CsvConnection csvConnection)
        {
            var fileContent =
                "first_name,last_name\nIngmar,Hesser\nCristobal,Poulson\nMerrili,Simoens\nAnthiathia,Tokley\nAthena,Wooldridge\nVasili,Sircombe\nTeressa,Stickler\nArdyth,Lanham\nAthene,Mulcahy\nMyrtia,O'Hartnedy\nCristobal,Mugford\nHalsey,Granger\nRahel,Weepers\nEvanne,Tipple\nArdelia,Rubel";
            return File(Encoding.ASCII.GetBytes(fileContent), "text/csv", $"{csvConnection.TableName}.csv");
        }

        [HttpDelete]
        [Route("sql/delete/{id:int}")]
        public IActionResult DeleteSql(int id)
        {
            return Ok("Table deleted successfully.");
        }
    }
}