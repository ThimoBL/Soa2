using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VersionControl.Interfaces
{
    public interface IGitStrategy
    {
        void Commit(string message);
        void Push();
        void Pull();
        void Branch(string branch);
        void Merge(string branch);
        void Checkout(string branch);
    }
}
