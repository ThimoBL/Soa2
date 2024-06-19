using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VersionControl.Interfaces
{
    public interface IVersionControl
    {
        void Commit(string message);
        void Push();
        string Pull();
        void Branch(string branch);
        void Merge(string branch);
        void Checkout(string branch);
        List<string> GetBranches();
        List<string> GetCommits(string branch);
        string GetCurrentBranch();
    }
}
