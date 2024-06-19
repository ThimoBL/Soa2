using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.VersionControl.Interfaces;

namespace Domain.VersionControl.Strategy
{
    public class OtherStrategy : IGitStrategy
    {
        public void Commit(string message)
        {
            throw new NotImplementedException();
        }

        public void Push()
        {
            throw new NotImplementedException();
        }

        public string Pull()
        {
            throw new NotImplementedException();
        }

        public void Branch(string branch)
        {
            throw new NotImplementedException();
        }

        public void Merge(string branch)
        {
            throw new NotImplementedException();
        }

        public void Checkout(string branch)
        {
            throw new NotImplementedException();
        }

        public List<string> GetBranches()
        {
            throw new NotImplementedException();
        }

        public List<string> GetCommits(string branch)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentBranch()
        {
            throw new NotImplementedException();
        }
    }
}