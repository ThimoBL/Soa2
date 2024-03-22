using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Pipelines.Visitor;

namespace Domain.Pipelines
{
    public abstract class PipelineCompositeActions : PipelineActions
    {
        private readonly List<PipelineActions> _actions = new();

        public void AddAction(PipelineActions action)
        {
            _actions.Add(action);
        }

        public void RemoveAction(PipelineActions action)
        {
            _actions.Remove(action);
        }

        public override void AcceptPipeline(IPipelineVisitor visitor)
        {
            foreach (var action in _actions)
            {
                action.AcceptPipeline(visitor);
            }
        }   
    }
}
