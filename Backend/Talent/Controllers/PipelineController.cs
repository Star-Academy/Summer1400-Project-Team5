using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talent.Data.Entities;
using Talent.Models;
using Talent.Models.Convertors;
using Talent.Services.Interfaces;

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
            var listPipelineModels = listPipelines.Select(PipelineConvertor.ConvertPipelineModel).ToList();
            return Ok(listPipelineModels);
        }

        [Route("[controller]/{pipelineId:int}")]
        [HttpGet]
        public async Task<IActionResult> GetPipeline(int pipelineId)
        {
            var pipelineModel = await GetPipelineModel(pipelineId);
            return pipelineModel == null ? NotFound("There is no pipeline with this id") : Ok(pipelineModel);
        }


        [Route("[controller]/kill/{pipelineId:int}")]
        [HttpPost]
        public void KillPipeline(int pipelineId)
        {
            CancelPipelineProcess(pipelineId);
            DeleteProcessInfo(pipelineId);
        }
        
        [Route("[controller]/status/{pipelineId:int}")]
        [HttpPost]
        public IActionResult PipelineStatus(int pipelineId)
        {
            var stat = GetProcessInfoModel(pipelineId);
            return Ok(stat);
        }

        private ProcessInfoModel GetProcessInfoModel(int pipelineId)
        {
            var processInfo = _unitOfWork.ProcessInfos[pipelineId];
            var stat = new ProcessInfoModel(processInfo);
            return stat;
        }

        private void CancelPipelineProcess(int pipelineId)
        {
            _unitOfWork.ProcessInfos[pipelineId].CancellationTokenSource.Cancel();
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
            PipelineModel pipelineModel = PipelineConvertor.ConvertPipelineModel(pipeline);
            return pipelineModel;
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User);
        }

        [HttpPost]
        [Route("[controller]/actions/{pipelineId:int}")]
        public async Task<IActionResult> Processes(int pipelineId, [FromBody] List<AggregationModel> aggregations, 
            [FromBody] List<FilterModel> filters, [FromBody] List<JoinModel> joins)
        {
            var pipelineProcess = await _unitOfWork.PipelineProcesses.GetAsync(p => p.PipelineId == pipelineId);
            var processors = await _unitOfWork.Processes
                .GetAllAsync(p => p.PipelineProcessId == pipelineProcess.PipelineProcessId);
            _unitOfWork.Processes.DeleteRange(processors);

            foreach (var aggregationModel in aggregations)
            {
                Processor aggregate = new Aggregation()
                {
                    Method = aggregationModel.Method,
                    AggregationColumn = aggregationModel.AggregateColumn,
                };
                await _unitOfWork.Processes.InsertAsync(aggregate);
                foreach (var groupColumn in aggregationModel.GroupColumns)
                {
                    await _unitOfWork.GroupByColumns.InsertAsync(new GroupByColumn()
                    {
                        AggregationId = aggregate.ProcessId,
                        ColumnName = groupColumn
                    });
                }
            }
            
            foreach (var joinModel in joins)
            {
                await _unitOfWork.Processes.InsertAsync(new Join()
                {
                    Index = joinModel.Index, 
                    ProcessType = joinModel.ProcessType,
                    PipelineProcessId = pipelineProcess.PipelineProcessId,
                    JoinMethod = joinModel.JoinMethod,
                    AddSourceId = joinModel.AddSourceId,
                    SourceKey = joinModel.SourceKey,
                    AddSourceKey = joinModel.AddSourceKey
                });
            }
            
            foreach (var filterModel in filters)
            {
                await _unitOfWork.Processes.InsertAsync(new Filter()
                {
                    Index = filterModel.Index,
                    ProcessType = filterModel.ProcessType,
                    PipelineProcessId = pipelineProcess.PipelineProcessId
                });
                //TODO: add Tree
            }

            var pipeline = await _unitOfWork.Pipelines.GetAsync(p => p.PipelineId == pipelineId);
            pipeline.RunDemo();

            return Ok(pipeline.Destination.OverView());
        }

        public async Task<IActionResult> Run(int pipelineId)
        {
            var pipeline = await _unitOfWork.Pipelines.GetAsync(p => p.PipelineId == pipelineId);
            RunPipelineAsync(pipeline);
            return Ok();
        }

        private async Task RunPipelineAsync(Pipeline pipeline)
        {
            await Task.Run(async () =>
            {
                int pipelineId = pipeline.PipelineId;
                var processInfo = CreateProcessInfo(pipelineId);
                await pipeline.Run(processInfo);
                DeleteProcessInfo(pipelineId);
            });
        }

        private void DeleteProcessInfo(int pipelineId)
        {
            _unitOfWork.ProcessInfos.Remove(pipelineId);
        }

        private ProcessInfo CreateProcessInfo(int pipelineId)
        {
            var processInfo = new ProcessInfo();
            _unitOfWork.ProcessInfos[pipelineId] = processInfo;
            return processInfo;
        }
    }
}