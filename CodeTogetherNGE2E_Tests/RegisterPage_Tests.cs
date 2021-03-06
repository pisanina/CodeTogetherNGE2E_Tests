﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CodeTogetherNGE2E_Tests
{
    internal class RegisterPage_Tests: TestsBase
    {
        private Registration_PageObject _register;

        [TestCase("Test@a.com", "Pamięć.11")]
        public void SuccessRegisterTestWithNonStandardCharacters(string email, string password)
        {
            _register.Insert_Email(email);
            _register.Insert_Password(password);
            _register.Insert_ConfirmPassword(password);
            _register.Click_Submit();

            Assert.True(driver.PageSource.Contains("Hello Test@a.com!"));
        }

        [TestCase("@a.com", "The Email field is not a valid e-mail address")]
        [TestCase("user@acom", "The Email field is not a valid e-mail address")]
        [TestCase("Not email", "The Email field is not a valid e-mail address")]
        public void WrongEmailTest(string email, string error)
        {
            _register.Insert_Email(email);
            _register.Insert_Password("");

            Assert.True(driver.PageSource.Contains(error));
        }

        [TestCase("Simply password", "Passwords must have at least one digit ('0'-'9').")]
        [TestCase("simply password123", "Passwords must have at least one uppercase ('A'-'Z').")]
        [TestCase("short", "The Password must be at least 6 and at max 100 characters long.")]
        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras ultricies molestie urna at massa nunc.9", "The Password must be at least 6 and at max 100 characters long.")]
        public void WrongPasswordTest(string password, string error)
        {
            _register.Insert_Email("SomeMail@a.com");
            _register.Insert_Password(password);
            _register.Insert_ConfirmPassword(password);
            _register.Click_Submit();

            Assert.True(driver.PageSource.Contains(error));
        }

        [Test]
        public void UserExist()
        {
            _register.Insert_Email("TestUser@a.com");
            _register.Insert_Password("Password.123");
            _register.Insert_ConfirmPassword("Password.123");
            _register.Click_Submit();

            Assert.True(driver.PageSource.Contains("User name 'TestUser@a.com' is already taken."));
        }

        [SetUp]
        public void SeleniumSetup()
        {
            base.Setup();

            _register = new Registration_PageObject(driver);

            _register.ClickCookieConsent();
            _register.GoToRegister();
        }

        [TearDown]
        public void DownSelenium()
        {
            driver.Quit();
        }
    }
}