using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CodeTogetherNGE2E_Tests
{
    public class Navigation_Tests
    {
        private IWebDriver _driver;
        private Navigation_PageObject _navigate;

        [Test]
        public void NavigationTests()
        {
            _navigate.GoToHome();
            Assert.True(_navigate.IsOnPage_Home());

            _navigate.GoToAbout();
            Assert.True(_navigate.IsOnPage_About());

            _navigate.GoToContact();
            Assert.True(_navigate.IsOnPage_Contact());

            _navigate.GoToProjectsGrid();
            Assert.True(_navigate.IsOnPage_ProjectsGrid());

            _navigate.GoToTechnologyList();
            Assert.True(_navigate.IsOnPage_TechnologyList());

            _navigate.GoToRegister();
            Assert.True(_navigate.IsOnPage_Register());

            _navigate.GoToLogin();
            Assert.True(_navigate.IsOnPage_Login());

            _navigate.GoToAddProject();
            Assert.True(_navigate.IsOnPage_Login());

            _navigate.LoginUser();

            _navigate.GoToAddProject();
            Assert.True(_navigate.IsOnPage_AddProject());
        }

        [SetUp]
        public void SeleniumSetup()
        {
            _driver = new ChromeDriver(Configuration.WebDriverLocation);
            _driver.Url = Configuration.WebApiUrl;
            _navigate = new Navigation_PageObject(_driver);
            _navigate.ClickCookieConsent();
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}