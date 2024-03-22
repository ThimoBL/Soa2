using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Pipelines.Visitor;

namespace Domain.Pipelines.Actions.AnalyzeAction.SonarCube
{
    public class SonarCubeReportingAction : PipelineActions
    {
        public override void AcceptPipeline(IPipelineVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
