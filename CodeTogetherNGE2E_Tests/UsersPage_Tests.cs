using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTogetherNGE2E_Tests
{
    class UsersPage_Tests: TestsBase
    {
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
        public void CheckUserDetails_TechnologySkill()
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
           
            Assert.True(_users.CheckOwnerCount("5", "TestUser@a.com"));
            Assert.True(_users.CheckMemberCount("0", "TestUser@a.com"));
            Assert.True(_users.CheckBeginnerCount("1", "TestUser@a.com"));
            Assert.True(_users.CheckAdvancedCount("1", "TestUser@a.com"));
            Assert.True(_users.CheckExpertCount("1", "TestUser@a.com"));
        }

        [Test]
        public void CheckUserDetails_OwnerMember()
        {
            _profile.GoToUsers();

            Assert.True(_users.CheckOwnerCount("5", "TestUser@a.com"));
            Assert.True(_users.CheckMemberCount("0", "TestUser@a.com"));
            Assert.True(_users.CheckOwnerCount("0", "coder@a.com"));
            Assert.True(_users.CheckMemberCount("0", "coder@a.com"));
            Assert.True(_users.CheckOwnerCount("1", "newcoder@a.com"));
            Assert.True(_users.CheckMemberCount("2", "newcoder@a.com"));
        }



        [SetUp]
        public void SeleniumSetup()
        {
            base.Setup();
           
            _users = new Users_PageObject(driver);
            _details = new Details_PageObject(driver);
            _profile = new Profile_PageObject(driver);

            _users.ClickCookieConsent();
        }

        [TearDown]
        public void DownSelenium()
        {
            driver.Quit();
        }
    }
}
