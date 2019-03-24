using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTogetherNGE2E_Tests
{
    class UsersPage_Tests
    {
        private IWebDriver _driver;
        private Users_PageObject _users;
        private Details_PageObject _details;
        private Profile_PageObject _profile;


        [Test]
        public void CheckLinksToProfile()
        {
            _users.GoToUsers();
            _users.ClickOnUser("TestUser@a.com");

            Assert.True(_users.IsOnPage_Profile());
            Assert.True(_profile.CheckUserNameInHeader("TestUser@a.com"));
        }


        [Test]
        public void CheckUserDetails()
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

            _profile.Logout();
            _users.GoToUsers();
           
            Assert.True(_users.CheckOwnerCount("6", "TestUser@a.com"));
            Assert.True(_users.CheckMemberCount("0", "TestUser@a.com"));
            Assert.True(_users.CheckBeginnerCount("1", "TestUser@a.com"));
            Assert.True(_users.CheckAdvancedCount("1", "TestUser@a.com"));
            Assert.True(_users.CheckExpertCount("1", "TestUser@a.com"));
        }



        [SetUp]
        public void SeleniumSetup()
        {
            _driver = new ChromeDriver(Configuration.WebDriverLocation);
            _driver.Url = Configuration.WebApiUrl;
           
            _users = new Users_PageObject(_driver);
            _details = new Details_PageObject(_driver);
            _profile = new Profile_PageObject(_driver);
            _users.PrepareDB();
            _users.ClickCookieConsent();
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}
