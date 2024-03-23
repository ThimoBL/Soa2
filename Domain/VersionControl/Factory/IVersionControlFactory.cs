using Domain.VersionControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VersionControl.Factory
{
    public interface IVersionControlFactory
    {
        IGitStrategy CreateGitStrategy(VersionControlTypes gitTypes);
    }
}
