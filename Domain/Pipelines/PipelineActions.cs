using Domain.Pipelines.Visitor;

namespace Domain.Pipelines
{
    public abstract class PipelineActions
    {
        public abstract void AcceptPipeline(IPipelineVisitor visitor);
    }
}
