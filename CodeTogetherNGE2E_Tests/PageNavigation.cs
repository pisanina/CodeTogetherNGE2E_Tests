using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace CodeTogetherNGE2E_Tests
{
    public class NavigationTest
    {
        private IWebDriver _driver;

        [SetUp]
        public void SeleniumSetup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            //  _driver.Url = "https://localhost:44362/";
            _driver.Url = "https://codetogetherng.azurewebsites.net/";
            _driver.FindElement(By.XPath("//*[@id=\"cookieConsent\"]/div/div[2]/div/button")).Click();
        }

        [Test]
        public void NavigationTests()
        {
            var Home = _driver.FindElement(By.Id("HomeTitle"));
            Assert.True(Home.Text == ("This side is under construction."));

            _driver.FindElement(By.Id("About")).Click();
            Assert.NotNull(_driver.FindElement(By.Id("AboutPage")));

            _driver.FindElement(By.Id("Contact")).Click();
            Assert.NotNull(_driver.FindElement(By.Id("ContactPage")));

            _driver.FindElement(By.Id("AddProject")).Click();
            Assert.NotNull(_driver.FindElement(By.Id("Title")));

            _driver.FindElement(By.Id("ProjectsGrid")).Click();
            Assert.NotNull(_driver.FindElement(By.Id("ProjectGridPage")));

            _driver.FindElement(By.Id("Register")).Click();
            Assert.NotNull(_driver.FindElement(By.Id("Input_ConfirmPassword")));

            _driver.FindElement(By.Id("Login")).Click();
            Assert.NotNull(_driver.FindElement(By.Id("Input_RememberMe")));
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}