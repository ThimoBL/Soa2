using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.VersionControl.Interfaces;

namespace Domain.VersionControl
{
    public class GitStrategy : IGitStrategy
    {
        //Key: Branch name, Value: list of commits
        private readonly Dictionary<string, List<string>> _repository = new();
        private string _currentBranch;

        public GitStrategy()
        {
            _repository.Add("master", new());
            _currentBranch = "master";
        }

        public void Commit(string message)
        {
            if (_repository.TryGetValue(_currentBranch, out var commits))
            {
                commits.Add(message);
                Console.WriteLine($"Changes successfully committed!");
                return;
            }

            Console.WriteLine("Branch does not exist");
        }

        public void Push()
        {
            if (_repository.ContainsKey(_currentBranch))
            {
                Console.WriteLine("Pushing changes to remote repository");
                return;
            }
            Console.WriteLine("Branch does not exist");
        }

        public void Pull()
        {
            if (_repository.ContainsKey(_currentBranch))
            {
                Console.WriteLine("Pulling changes from remote repository");
                return;
            }

            Console.WriteLine("Branch does not exist");
        }

        public void Branch(string branch)
        {
            if (_repository.ContainsKey(branch))
            {
                Console.WriteLine("Branch already exists");
                return;
            }

            _repository.Add(branch, new());
        }

        public void Merge(string branch)
        {
            if (_repository.TryGetValue(_currentBranch, out var commits))
            {
                commits.AddRange(_repository[branch]);
                Console.WriteLine("Branch merged successfully");
                return;
            }

            Console.WriteLine("Branch does not exist");
        }

        public void Checkout(string branch)
        {
            if (!_repository.ContainsKey(branch))
            {
                Console.WriteLine("Branch does not exist");
                return;
            }

            _currentBranch = branch;
            Console.WriteLine($"Switched to branch: {branch}");
        }
    }
}