using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CodeTogetherNGE2E_Tests
{
    internal class ProfilePage_Tests : TestsBase
    {
        private Profile_PageObject _profile;
        private Users_PageObject _users;
        private Details_PageObject _details;

        [Test]
        public void CheckAddingSkill()
        {
            _profile.LoginOwner();
            _profile.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");
            _profile.SelectTechnology(6);
            _profile.SelectSkillLevel(1);
            _profile.ClickOnAdd();

            _profile.SelectTechnology(1);
            _profile.SelectSkillLevel(2);
            _profile.ClickOnAdd();

            _profile.SelectTechnology(3);
            _profile.SelectSkillLevel(3);
            _profile.ClickOnAdd();

            Assert.True(_profile.HowManySkills() == 3);
            Assert.True(_profile.CheckUserNameInHeader("TestUser@a.com"));
            Assert.True(_profile.CheckNameOfSkill("Java"));
            Assert.True(_profile.CheckLevelOfSkill("Java", "Beginner"));

            Assert.True(_profile.CheckNameOfSkill("Angular"));
            Assert.True(_profile.CheckLevelOfSkill("Angular", "Advanced"));

            Assert.True(_profile.CheckNameOfSkill("C"));
            Assert.True(_profile.CheckLevelOfSkill("C", "Expert"));
        }


        [Test]
        public void CheckAddingSkillThatExist()
        {
            _profile.LoginOwner();
            _profile.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");
            _profile.SelectTechnology(6);
            _profile.SelectSkillLevel(1);
            _profile.ClickOnAdd();

            _profile.SelectTechnology(6);
            _profile.SelectSkillLevel(2);
            _profile.ClickOnAdd();

            Assert.True(_profile.HowManySkills() == 1);
            Assert.True(_profile.CheckUserNameInHeader("TestUser@a.com"));
            Assert.True(_profile.CheckNameOfSkill("Java"));
            Assert.True(_profile.CheckLevelOfSkill("Java", "Advanced"));
        }

        [Test]
        public void CheckDeletingSkill()
        {
            _profile.LoginOwner();
            _profile.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");
            _profile.SelectTechnology(6);
            _profile.SelectSkillLevel(1);
            _profile.ClickOnAdd();

            _profile.ClickOnDelete("Java");

            Assert.True(_profile.IsNoSkillsMessageDisplayed());
        }


        [Test]
        public void CheckAddingITRoles()
        {
            _profile.LoginOwner();
            _profile.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");
            _profile.SelectITRole(1);
            _profile.ClickOnAddRole();

            _profile.SelectITRole(5);
            _profile.ClickOnAddRole();


            Assert.True(_profile.HowManyITRoles() == 2);
            Assert.True(_profile.CheckITRoleAdded("Automation Tester"));
            Assert.True(_profile.CheckITRoleAdded("DBA"));
        }

        [Test]
        public void CheckAddingITRolesThatExist()
        {
            _profile.LoginOwner();
            _profile.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");
            _profile.SelectITRole(1);
            _profile.ClickOnAddRole();

            _profile.SelectITRole(1);
            _profile.ClickOnAddRole();


            Assert.True(_profile.HowManyITRoles() == 1);
            Assert.True(_profile.CheckITRoleAdded("Automation Tester"));
        }

        [Test]
        public void CheckDeletingITRoles()
        {
            _profile.LoginOwner();
            _profile.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");
            _profile.SelectITRole(1);
            _profile.ClickOnAddRole();

            _profile.SelectITRole(5);
            _profile.ClickOnAddRole();


            Assert.True(_profile.HowManyITRoles() == 2);
            Assert.True(_profile.CheckITRoleAdded("Automation Tester"));
            Assert.True(_profile.CheckITRoleAdded("DBA"));

            _profile.ClickOnDeleteITRole("DBA");
            Assert.True(_profile.HowManyITRoles() == 1);

            _profile.ClickOnDeleteITRole("Automation Tester");
            Assert.True(_profile.IsNoITRoleMessageDisplayed());
        }



        [Test]
        public void AreLinksToProjectsDetailsWorks()
        {
            _profile.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");
            _profile.ClickOnProjectTitle("FirstProject");
            Assert.True(_details.GetTitle() == "FirstProject");
        }

        [Test]
        public void NotLoggedUserOnProfile() 
        {
            _profile.LoginCoder();
            _profile.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");

            Assert.False(_profile.IsAddButtonDisplayed());
            Assert.False(_profile.IsTechListAddDisplayed());
            Assert.False(_profile.IsSkillLevelAddDisplayed());
            Assert.False(_profile.IsITRoleListAddDisplayed());
            Assert.False(_profile.IsAddITRoleButtonDisplayed());
           
            Assert.True(_profile.IsProjectDisplayed("FirstProject"));
        }


        [Test]
        public void AnotherUserOnProfile() 
        {
            _profile.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");

            Assert.False(_profile.IsAddButtonDisplayed());
            Assert.False(_profile.IsTechListAddDisplayed());
            Assert.False(_profile.IsSkillLevelAddDisplayed());
            Assert.False(_profile.IsITRoleListAddDisplayed());
            Assert.False(_profile.IsAddITRoleButtonDisplayed());

            Assert.True(_profile.IsProjectDisplayed("FirstProject"));
        }


        [Test]
        public void NoSkillsNoProjectsNoRoles()
        {
            _profile.LoginCoder();
            _profile.GoToUsers();
            _users.ClickOnUser("coder@a.com");

            Assert.True(_profile.IsNoSkillsMessageDisplayed());
            Assert.True(_profile.IsNoProjectMessageDisplayed());
            Assert.True(_profile.IsNoITRoleMessageDisplayed());
        }

        [Test]
        public void CheckProjectsOfNewCoder()
        {
            _profile.GoToUsers();
            _users.ClickOnUser("newcoder@a.com");
            Assert.True(_profile.HowManyProjects() == 3);
            Assert.True(_profile.IsProjectDisplayed("SecondProject"));
            Assert.True(_profile.IsProjectDisplayed("Project with Two Tech"));
            Assert.True(_profile.IsProjectDisplayed("Polish title like pamięć"));
        }

        [SetUp]
        public void SeleniumSetup()
        {
            base.Setup();

            _profile = new Profile_PageObject(driver);
            _users = new Users_PageObject(driver);
            _details = new Details_PageObject(driver);

            _profile.ClickCookieConsent();
        }

        [TearDown]
        public void DownSelenium()
        {
            driver.Quit();
        }
    }
}