using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talent.Data.Entities;
using Talent.Models;

namespace Talent.Controllers.MockedControllers
{
    [Authorize]
    [Route("[controller]")]
    public class MockPipelineController : ControllerBase
    {
        public MockPipelineController()
        {  }

        private PipelineModel GetPipelineModel()
        {
            var processes = new List<Processor>();
            processes.Add(new Join() {});
            processes.Add(new Filter() {});
            processes.Add(new Aggregation() {});

            return new PipelineModel()
            {
                DestinationId = 1,
                Name = "Pipeline1",
                NumberOfProcesses = 3,
                PipelineId = 1,
                SourceId = 2,
                Processors = processes
            };
        }

        [Route("")]
        [HttpGet]
        public IActionResult Get()
        {
            var listPipelineModels = new List<PipelineModel>();
            listPipelineModels.Add(GetPipelineModel());
            return Ok(listPipelineModels);
        }

        [Route("{pipelineId:int}")]
        [HttpGet]
        public IActionResult GetPipeline(int pipelineId)
        {
            return Ok(GetPipelineModel());
        }


        [Route("kill/{pipelineId:int}")]
        [HttpPost]
        public IActionResult KillPipeline(int pipelineId)
        {
            return Ok();
        }
        
        [Route("status/{pipelineId:int}")]
        [HttpPost]
        public IActionResult PipelineStatus(int pipelineId)
        {
            return Ok(new ProcessInfoModel(new ProcessInfo()
            {
                CurrentProcessIndex = 2,
            }));
        }

        [Route("")]
        [HttpPost]
        public IActionResult Post([FromBody] string name, [FromBody] int sourceId,
            [FromBody] int destinationId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("actions/{pipelineId:int}")]
        public IActionResult Processes(int pipelineId, [FromBody] List<AggregationModel> aggregations, 
            [FromBody] List<FilterModel> filters, [FromBody] List<JoinModel> joins)
        {
            return Ok();
        }

        public IActionResult Run(int pipelineId)
        {
            return Ok();
        }
        public IActionResult CeateYamlFile(int pipelineId)
        {
            return Ok();            
        }
    }
}