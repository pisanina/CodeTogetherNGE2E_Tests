using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace CodeTogetherNGE2E_Tests
{
    class GridPage_tests
    {
        private IWebDriver _driver;
      

        [TestCase("Project")] // Search in Title
        [TestCase("Yes")]  // Search in Description
        [TestCase("ć")]   // Test for foreign languages

        public void SearchProject(string ToSearch)
        {
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.True(_driver.PageSource.Contains("Funny"));
            Assert.True(_driver.PageSource.Contains(ToSearch));

            _driver.FindElement(By.Id("SearchInput")).SendKeys(ToSearch);
            _driver.FindElement(By.Id("SearchButton")).Click();

            Assert.False(_driver.PageSource.Contains("Funny"));
            Assert.True(_driver.PageSource.Contains(ToSearch));
        }

        [Test]
        public void DetailView()
        {
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.True(_driver.PageSource.Contains("Search"));

            _driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[2]/td[1]/a/span")).Click();

            Assert.False(_driver.PageSource.Contains("Search"));
            Assert.True(_driver.PageSource.Contains("CreationDate"));
        }


        [Test]
        public void DetailViewBeforeSearch()
        {
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.True(_driver.PageSource.Contains("Test"));

            _driver.FindElement(By.Id("SearchInput")).SendKeys("Funny");
            _driver.FindElement(By.Id("SearchInput")).SendKeys(Keys.Enter);

            Assert.False(_driver.PageSource.Contains("Test"));

            _driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[2]/td[1]/a/span")).Click();

            Assert.False(_driver.PageSource.Contains("Search"));
            Assert.True(_driver.PageSource.Contains("CreationDate"));
            Assert.True(_driver.PageSource.Contains("Funny"));
        }

        [SetUp]
        public void SeleniumSetup()
        {
            _driver = new ChromeDriver(Configuration.WebDriverLocation);

            _driver.Url = Configuration.WebApiUrl;

           // Add = new AddProject_TestsPageObject(_driver);

            _driver.FindElement(By.XPath("//*[@id=\"cookieConsent\"]/div/div[2]/div/button")).Click();
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }

    }
}
