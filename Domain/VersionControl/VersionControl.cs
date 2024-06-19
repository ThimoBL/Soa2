using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.VersionControl.Factory;
using Domain.VersionControl.Interfaces;

namespace Domain.VersionControl
{
    public class VersionControl : IVersionControl
    {
        private readonly IGitStrategy _gitStrategy;

        public VersionControl(IVersionControlFactory versionControlFactory)
        {
            _gitStrategy = versionControlFactory.CreateGitStrategy(VersionControlTypes.Git);
        }

        public void Commit(string message)
        {
            _gitStrategy.Commit(message);
        }

        public void Push()
        {
            _gitStrategy.Push();
        }

        public string Pull()
        {
            return _gitStrategy.Pull();
        }

        public void Branch(string branch)
        {
            _gitStrategy.Branch(branch);
        }

        public void Merge(string branch)
        {
            _gitStrategy.Merge(branch);
        }

        public void Checkout(string branch)
        {
            _gitStrategy.Checkout(branch);
        }

        public List<string> GetBranches()
        {
            return _gitStrategy.GetBranches();
        }

        public List<string> GetCommits(string branch)
        {
            return _gitStrategy.GetCommits(branch);
        }

        public string GetCurrentBranch()
        {
            return _gitStrategy.GetCurrentBranch();
        }
    }
}
