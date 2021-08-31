using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talent.Data.Entities;
using Talent.Models;
using Talent.Services.Interfaces;
using Talent.Services.Repositories;

namespace Talent.Controllers
{
    [Authorize]
    public class PipelineController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public PipelineController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Route("[controller]")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userId = _userManager.GetUserId(User);
            var listPipelines = await _unitOfWork.Pipelines.GetAllAsync(p => p.OwnerId == userId);
            var listPipelineModels = listPipelines.Select(PipelineAdapter.GetPipelineModel).ToList();
            return Ok(listPipelineModels);
        }

        [Route("[controller]/{pipelineId:int}")]
        [HttpGet]
        public async Task<IActionResult> GetPipeline(int pipelineId)
        {
            var pipelineModel = await GetPipelineModel(pipelineId);
            return pipelineModel == null ? NotFound("There is no pipeline with this id") : Ok(pipelineModel);
        }


        [Route("[controller]")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string name, [FromBody] int sourceId,
            [FromBody] int destinationId)
        {
            var pipeline = CreatePipeline(name, sourceId, destinationId);
            await AddPipelineToDatabase(pipeline);
            return Ok();
        }

        private async Task AddPipelineToDatabase(Pipeline pipeline)
        {
            await _unitOfWork.Pipelines.InsertAsync(pipeline);
            await _unitOfWork.PipelineProcesses.InsertAsync(pipeline.Process);
            await _unitOfWork.Save();
        }

        private Pipeline CreatePipeline(string name, int sourceId, int destinationId)
        {
            Pipeline pipeline = new Pipeline();
            pipeline.Name = name;
            pipeline.DestinationId = destinationId;
            pipeline.SourceId = sourceId;
            pipeline.Process = new PipelineProcess();
            return pipeline;
        }


        private async Task<PipelineModel> GetPipelineModel(int pipelineId)
        {
            string userId = GetUserId();
            var pipeline = await _unitOfWork.Pipelines
                .GetAsync(p => p.OwnerId == userId && p.PipelineId == pipelineId);
            PipelineModel pipelineModel = PipelineAdapter.GetPipelineModel(pipeline);
            return pipelineModel;
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User);
        }

        [HttpPost]
        [Route("[controller]/actions/{pipelineId:int}")]
        public async Task<IActionResult> Processes(int pipelineId, [FromBody] IList<ProcessModel> processModels)
        {
            var pipelineProcess = await _unitOfWork.PipelineProcesses.GetAsync(p => p.PipelineId == pipelineId);
            var processors = await _unitOfWork.Processes
                .GetAllAsync(p => p.PipelineProcessId == pipelineProcess.PipelineProcessId);
            _unitOfWork.Processes.DeleteRange(processors);
        }
    }
}