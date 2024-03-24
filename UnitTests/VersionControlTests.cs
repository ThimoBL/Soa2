using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.GeneralModels;
using Domain.Sprints.Factory;
using Domain.VersionControl;
using Domain.VersionControl.Factory;
using Moq;

namespace UnitTests
{
    public class VersionControlTests
    {
        [Fact]
        public void User_Can_Use_VersionControl()
        {
            //Arrange
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            //Act
            var versionControl = project.GetGitStrategy();


            //Assert
            Assert.IsType<GitStrategy>(versionControl);
        }

        [Fact]
        public void User_Can_Commit_Using_VersionControl()
        {
            //Arrange
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());
            var git = project.GetGitStrategy();

            git.Branch("Dev");
            git.Checkout("Dev");

            //Act
            git.Commit("Initial commit");

            //Assert
            Assert.Contains("Initial commit", git.GetCommits("Dev"));
        }

        [Fact]
        public void User_Can_Push_Using_VersionControl()
        {
            //Arrange
            var versionControl = new Mock<IVersionControlFactory>();
            versionControl.Setup(x => x.CreateGitStrategy(VersionControlTypes.Git))
                .Returns(new GitStrategy());
            var git = versionControl.Object.CreateGitStrategy(VersionControlTypes.Git);

            git.Branch("Dev");
            git.Checkout("Dev");
            git.Commit("Initial commit");

            //Act
            git.Push();

            //Assert
            Assert.Contains("Initial commit", git.GetCommits("Dev"));
        }

        [Fact]
        public void User_Can_Pull_Using_VersionControl()
        {
            //Arrange
            var versionControl = new Mock<IVersionControlFactory>();
            versionControl.Setup(x => x.CreateGitStrategy(VersionControlTypes.Git))
                .Returns(new GitStrategy());
            var git = versionControl.Object.CreateGitStrategy(VersionControlTypes.Git);

            git.Branch("Dev");
            git.Checkout("Dev");
            git.Commit("Initial commit");

            //Act
            var lastChange = git.Pull();

            //Assert
            Assert.Equal("Initial commit", lastChange);
        }

        [Fact]
        public void User_Can_CreateBranch_Using_VersionControl()
        {
            //Arrange
            var versionControl = new Mock<IVersionControlFactory>();
            versionControl.Setup(x => x.CreateGitStrategy(VersionControlTypes.Git))
                .Returns(new GitStrategy());
            var git = versionControl.Object.CreateGitStrategy(VersionControlTypes.Git);

            //Act
            git.Branch("Dev");

            //Assert
            Assert.Contains("Dev", git.GetBranches());
        }

        [Fact]
        public void User_Can_MergeBranch_Using_VersionControl()
        {
            //Arrange
            var versionControl = new Mock<IVersionControlFactory>();
            versionControl.Setup(x => x.CreateGitStrategy(VersionControlTypes.Git))
                .Returns(new GitStrategy());
            var git = versionControl.Object.CreateGitStrategy(VersionControlTypes.Git);

            git.Branch("Dev");
            git.Branch("Feature");
            git.Checkout("Feature");
            git.Commit("Feature commit");
            git.Checkout("Dev");
            git.Commit("Initial commit");

            //Act
            git.Merge("Feature");

            //Assert
            Assert.Contains("Feature commit", git.GetCommits("Dev"));
        }

        [Fact]
        public void User_Can_CheckoutBranch_Using_VersionControl()
        {
            //Arrange
            var versionControl = new Mock<IVersionControlFactory>();
            versionControl.Setup(x => x.CreateGitStrategy(VersionControlTypes.Git))
                .Returns(new GitStrategy());
            var git = versionControl.Object.CreateGitStrategy(VersionControlTypes.Git);

            git.Branch("Dev");
            git.Branch("Feature");

            //Act
            git.Checkout("Feature");

            //Assert
            Assert.Equal("Feature", git.GetCurrentBranch());
        }
    }
}