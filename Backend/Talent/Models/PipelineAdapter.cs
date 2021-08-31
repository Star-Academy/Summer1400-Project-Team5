using System;
using Talent.Data.Entities;

namespace Talent.Models
{
    public static class PipelineAdapter
    {
        public static Pipeline GetPipeline(PipelineModel pipelineModel)
        {
            return new Pipeline()
            {
                Name = pipelineModel.Name,
                DestinationId = pipelineModel.DestinationId,
                SourceId = pipelineModel.SourceId,
                OwnerId = 
            };
        }
        public static PipelineModel GetPipelineModel(Pipeline pipelineModel)
        {
            return new PipelineModel()
            {
                Name = pipelineModel.Name,
                Processors = pipelineModel.Process.Processes,
                DestinationId = pipelineModel.DestinationId,
                SourceId = pipelineModel.SourceId,
                NumberOfProcesses = pipelineModel.Process.Processes.Count
            };
        }
    }
}