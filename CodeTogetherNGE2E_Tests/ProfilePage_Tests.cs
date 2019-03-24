using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CodeTogetherNGE2E_Tests
{
    internal class ProfilePage_Tests
    {
        private IWebDriver _driver;
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

            Assert.True(_profile.CheckUserNameInHeader("TestUser@a.com"));
            Assert.True(_profile.CheckNameOfSkill("Java"));
            Assert.True(_profile.CheckLevelOfSkill("Java", "Beginner"));

            Assert.True(_profile.CheckNameOfSkill("Angular"));
            Assert.True(_profile.CheckLevelOfSkill("Angular", "Advanced"));

            Assert.True(_profile.CheckNameOfSkill("C"));
            Assert.True(_profile.CheckLevelOfSkill("C", "Expert"));
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
            _profile.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");

            Assert.False(_profile.IsAddButtonDisplayed());
            Assert.False(_profile.IsTechListAddDisplayed());
            Assert.False(_profile.IsSkillLevelAddDisplayed());
           
            Assert.True(_profile.IsProjectDisplayed("FirstProject"));
        }

        [Test]
        public void NoSkillsNoProjects()
        {
            _profile.LoginCoder();
            _profile.GoToUsers();
            _users.ClickOnUser("coder@a.com");

            Assert.True(_profile.IsNoSkillsMessageDisplayed());
            Assert.True(_profile.IsNoProjectMessageDisplayed());
        }


        [SetUp]
        public void SeleniumSetup()
        {
            _driver = new ChromeDriver(Configuration.WebDriverLocation);
            _driver.Url = Configuration.WebApiUrl;
            _profile = new Profile_PageObject(_driver);
            _users = new Users_PageObject(_driver);
            _details = new Details_PageObject(_driver);
            _profile.PrepareDB();
            _profile.ClickCookieConsent();
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}