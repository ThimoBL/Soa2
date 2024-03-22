using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Pipelines.Visitor;

namespace Domain.Pipelines
{
    public class Pipeline(string name) : PipelineCompositeActions
    {
        public string Name { get; set; } = name;
    }
}
