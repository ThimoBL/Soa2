using Domain.Pipelines.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pipelines.Actions.SourceAction
{
    public class SourceCompositeAction : PipelineCompositeActions
    {
        public override void AcceptPipeline(IPipelineVisitor visitor)
        {
            visitor.Visit(this);
            base.AcceptPipeline(visitor);
        }
    }
}
