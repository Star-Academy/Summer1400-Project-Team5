using Talent.Data.Entities;

namespace Talent.Models.Convertors
{
    public static class PipelineConvertor
    {
        public static Pipeline ConvertPipeline(PipelineModel pipelineModel)
        {
            return new Pipeline()
            {
                // Name = pipelineModel.Name,
                // DestinationId = pipelineModel.DestinationId,
                // SourceId = pipelineModel.SourceId,
                // //OwnerId = //TODO
            };
        }
        public static PipelineModel ConvertPipelineModel(Pipeline pipelineModel)
        {
            return new PipelineModel()
            {
                // Name = pipelineModel.Name,
                // Processors = pipelineModel.Process.Processes,
                // DestinationId = pipelineModel.DestinationId,
                // SourceId = pipelineModel.SourceId,
                // NumberOfProcesses = pipelineModel.Process.Processes.Count
            };
        }
    }
}