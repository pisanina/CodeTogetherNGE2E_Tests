using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CodeTogetherNGE2E_Tests
{
    internal class RegisterPage_Tests
    {
        private IWebDriver _driver;
        IWebElement emailInput;
        IWebElement passwordInput;
        IWebElement confirmPasswordInput;
        IWebElement submitButton;

        [TestCase("Test@a.com", "Pamięć.11")]
        public void SuccessRegisterTestWithNonStandardCharacters(string email, string password)
        {
            emailInput.SendKeys(email);
            passwordInput.SendKeys(password);
            confirmPasswordInput.SendKeys(password);
            submitButton.Click();

            Assert.True(_driver.PageSource.Contains("Hello Test@a.com!"));
        }

     
        [TestCase("@a.com", "The Email field is not a valid e-mail address")]
        [TestCase("user@acom", "The Email field is not a valid e-mail address")]
        [TestCase("Not email", "The Email field is not a valid e-mail address")]
        public void WrongEmailTest(string email, string error)
        {
            emailInput.SendKeys(email);
            passwordInput.Click();

            Assert.True(_driver.PageSource.Contains(error));
        }

        [TestCase("Simply password", "Passwords must have at least one digit ('0'-'9').")]
        [TestCase("simply password123", "Passwords must have at least one uppercase ('A'-'Z').")]
        [TestCase("short", "The Password must be at least 6 and at max 100 characters long.")]
        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras ultricies molestie urna at massa nunc.9", "The Password must be at least 6 and at max 100 characters long.")]
        public void WrongPasswordTest(string password, string error)
        {
            emailInput.SendKeys("SomeMail@a.com");
            passwordInput.SendKeys(password);
            confirmPasswordInput.SendKeys(password);
            submitButton.Click();

            Assert.True(_driver.PageSource.Contains(error));
        }

        [Test]
        public void UserExist()
        {
            emailInput.SendKeys("TestUser@a.com");
            passwordInput.SendKeys("Password.123");
            confirmPasswordInput.SendKeys("Password.123");
            submitButton.Click();

            Assert.True(_driver.PageSource.Contains("User name 'TestUser@a.com' is already taken."));
        }

        [SetUp]
        public void SeleniumSetup()
        {
            AddProject_TestsPageObject.PrepareDB();

            _driver = new ChromeDriver(Configuration.WebDriverLocation);
            _driver.Url = Configuration.WebApiUrl;
            _driver.FindElement(By.XPath("//*[@id=\"cookieConsent\"]/div/div[2]/div/button")).Click();
            _driver.FindElement(By.Id("Register")).Click();

            emailInput = _driver.FindElement(By.Id("Input_Email"));
            passwordInput = _driver.FindElement(By.Id("Input_Password"));
            confirmPasswordInput = _driver.FindElement(By.Id("Input_ConfirmPassword"));
            submitButton = _driver.FindElement(By.XPath("/html/body/div/div/div/form/button"));
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}