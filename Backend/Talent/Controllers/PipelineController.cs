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
        [Authorize]
        public async Task<IActionResult> Get()
        {
            string userId = _userManager.GetUserId(User);
            var listPipelines = await _unitOfWork.Pipelines.GetAll(p => p.OwnerId == userId);
            var listPipelineModels = listPipelines.Select(PipelineAdapter.GetPipelineModel).ToList();
            return Ok(listPipelineModels);
        }

        [Route("[controller]/{pipelineId:int}")]
        [Authorize]
        public async Task<IActionResult> GetPipeline(int pipelineId)
        {
            var pipelineModel = await GetPipelineModel(pipelineId);
            return pipelineModel == null ? NotFound("There is no pipeline with this id") : Ok(pipelineModel);
        }
        
        private async Task<PipelineModel> GetPipelineModel(int pipelineId)
        {
            string userId = _userManager.GetUserId(User);
            var pipeline = await _unitOfWork.Pipelines
                .Get(p => p.OwnerId == userId && p.PipelineId == pipelineId);
            PipelineModel pipelineModel = PipelineAdapter.GetPipelineModel(pipeline);
            return pipelineModel;
        }
    }
}