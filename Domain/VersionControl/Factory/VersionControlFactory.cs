using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.VersionControl.Interfaces;

namespace Domain.VersionControl.Factory
{
    public class VersionControlFactory : IVersionControlFactory
    {
        public IGitStrategy CreateGitStrategy(VersionControlTypes gitTypes) =>
            gitTypes switch
            {
                VersionControlTypes.Git => new GitStrategy(),
                _ => throw new NotImplementedException()
            };
    }
}
