using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talent.Data.Entities;
using Talent.Models;
using Talent.Models.Convertors;
using Talent.Services.Interfaces;
using YamlDotNet.RepresentationModel;

namespace Talent.Controllers
{
   // [Authorize]
   [Route("pipeline/")]
   public class PipelineController : ControllerBase
   {
       private readonly IUnitOfWork _unitOfWork;
       private readonly UserManager<AppUser> _userManager;
       private readonly ISqlHandler _sqlHandler;

       public PipelineController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager,
           ISqlHandler sqlHandler)
       {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _sqlHandler = sqlHandler;
       }

       [Route("pipeline-list")]
       [HttpGet]
       public IActionResult GetPipelinesList()
       {
           var userId = _userManager.GetUserId(User);
           var listPipelines = _unitOfWork.Pipelines.GetAllAsync(p => p.Owner.Id == userId).Result;
           return Ok(listPipelines);
       }

       [Route("pipeline/{pipelineId:int}")]
       [HttpGet]
       public async Task<IActionResult> GetPipeline(int pipelineId)
       {
           var pipelineModel = await GetPipelineModel(pipelineId);
           return pipelineModel == null ? NotFound("There is no pipeline with this id") : Ok(pipelineModel);
       }


       [Route("kill/{pipelineId:int}")]
       [HttpPost]
       public void KillPipeline(int pipelineId)
       {
           CancelPipelineProcess(pipelineId);
           DeleteProcessInfo(pipelineId);
       }

       [Route("status/{pipelineId:int}")]
       [HttpPost]
       public IActionResult GetPipelineStatus(int pipelineId)
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


       [Route("create-pipeline")]
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
           pipeline.Destination = _unitOfWork.DataSources.GetAsync(d => d.Id == destinationId).Result;
           pipeline.Source = _unitOfWork.DataSources.GetAsync(d => d.Id == sourceId).Result;
           pipeline.Process = new PipelineProcess();
           return pipeline;
       }


       private async Task<PipelineModel> GetPipelineModel(int pipelineId)
       {
           string userId = GetUserId();
           var pipeline = await _unitOfWork.Pipelines
               .GetAsync(p => p.Owner.Id == userId && p.PipelineId == pipelineId);
           PipelineModel pipelineModel = PipelineConvertor.ConvertPipelineModel(pipeline);
           return pipelineModel;
       }

       private string GetUserId()
       {
           return _userManager.GetUserId(User);
       }

       [HttpPost]
       [Route("update-pipeline/{pipelineId:int}")]
       public async Task<IActionResult> UpdatePipeline(int pipelineId, [FromBody] List<AggregationModel> aggregations,
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
                   PipelineProcessId = pipelineProcess.PipelineProcessId,
                   FilterSQL = filterModel.ToSql()
               });
           }

           var pipeline = await _unitOfWork.Pipelines.GetAsync(p => p.PipelineId == pipelineId);
           pipeline.RunDemo(_sqlHandler);

           return Ok();
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
               await pipeline.Run(processInfo, _sqlHandler);
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

       public async Task CreateYamlFile(int pipelineId)
       {
           var userId = GetUserId();
           var pipeline = await _unitOfWork.Pipelines
               .GetAsync(p => p.Owner.Id == userId && p.PipelineId == pipelineId);

           const string initialContent = "---\nversion: 1\n...";

           var stringReader = new StringReader(initialContent);
           var stream = new YamlStream();
           stream.Load(stringReader);

           var rootMappingNode = (YamlMappingNode)stream.Documents[0].RootNode;
           var details = new YamlMappingNode();
           details.Add("Name", pipeline.Name);
           details.Add("OwnerId", pipeline.Owner.Id);
           details.Add("Destination", pipeline.Destination.Id.ToString());
           details.Add("PipelineId", pipeline.PipelineId.ToString());
           details.Add("SourceId", pipeline.Source.Id.ToString());
           var process = new YamlMappingNode();
           var allProcesses = new YamlMappingNode();
           var processes = pipeline.Process.Processes;
           foreach (var processor in processes)
           {
               var thisProcessor = new YamlMappingNode();
               thisProcessor.Add("Index", processor.Index.ToString());
               thisProcessor.Add("ProcessType", processor.ProcessType.ToString());
               thisProcessor.Add("ProcessId", processor.ProcessId.ToString());
               thisProcessor.Add("PipelineProcessId", processor.PipelineProcessId.ToString());

               var pipelineProcess = new YamlMappingNode();
               pipelineProcess.Add("PipelineId" ,processor.PipelineProcess.PipelineId.ToString());
               pipelineProcess.Add("PipelineProcessId" ,processor.PipelineProcess.PipelineProcessId.ToString());
               thisProcessor.Add("PipelineProcess", pipelineProcess);

               allProcesses.Add(processor.ProcessId.ToString(), thisProcessor);
           }
           process.Add("Processes", allProcesses);
           process.Add("PipelineId", pipeline.Process.PipelineId.ToString());
           process.Add("PipelineProcessId", pipeline.Process.PipelineProcessId.ToString());
           details.Add("Process", process);

           rootMappingNode.Add("Pipeline", details);

           //await using TextWriter writer = System.IO.File.CreateText("C:\\temp\\test.yaml");
           //stream.Save(writer, false);

       }
   }
}