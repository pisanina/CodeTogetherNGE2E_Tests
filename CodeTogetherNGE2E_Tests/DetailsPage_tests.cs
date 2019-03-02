using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace CodeTogetherNGE2E_Tests
{
    internal class DetailsPage_Tests
    {
        private IWebDriver _driver;

        [Test]
        public void RedirectToDetailView()
        {
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.True(_driver.PageSource.Contains("Search"));

            _driver.FindElement(By.XPath("/html/body/div/div/div[1]/a")).Click();

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

            _driver.FindElement(By.XPath("/html/body/div/div/div[1]/a")).Click();

            Assert.False(_driver.PageSource.Contains("Search"));
            Assert.True(_driver.FindElement(By.Id("Title")).GetAttribute("value") == "Funny bunny");
            Assert.True(_driver.FindElement(By.Id("Owner")).GetAttribute("value") == "TestUser@a.com");
            Assert.True(_driver.FindElement(By.Id("Description")).GetAttribute("value") == "We want to create web aplication with many funny bunes");
            Assert.True(_driver.FindElement(By.Id("CreationDate")).GetAttribute("value") == DateTime.Now.ToString("dd/MM/yyy"));
            Assert.True(_driver.FindElement(By.Id("NewMembers")).GetAttribute("checked") == "true");
        }

        [Test]
        public void DetailViewTechnology()
        {
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.True(_driver.PageSource.Contains("Funny"));

            _driver.FindElement(By.Id("SearchInput")).SendKeys("Two Tech (Test)");
            _driver.FindElement(By.Id("SearchInput")).SendKeys(Keys.Enter);

            Assert.False(_driver.PageSource.Contains("Funny"));

            _driver.FindElement(By.XPath("/html/body/div/div/div/a")).Click();
            var  technology_C = _driver.FindElement(By.CssSelector("option[value='3']"));

            Assert.False(_driver.PageSource.Contains("Search"));
            Assert.True(_driver.PageSource.Contains("Project with Two Tech (Test)"));
            Assert.True(_driver.PageSource.Contains("value=\"6\" selected=\"\">Java"));
            Assert.True(_driver.PageSource.Contains("value=\"7\" selected=\"\">JavaScript"));
            Assert.True(_driver.PageSource.Contains("<option value=\"4\">C++</option>"));
            Assert.True(_driver.PageSource.Contains("TestUser@a.com"));
            Assert.True(technology_C.Selected == false);
        }

        [SetUp]
        public void SeleniumSetup()
        {
            AddProject_TestsPageObject.PrepareDB();
            _driver = new ChromeDriver(Configuration.WebDriverLocation);
            _driver.Url = Configuration.WebApiUrl;
            _driver.FindElement(By.XPath("//*[@id=\"cookieConsent\"]/div/div[2]/div/button")).Click();
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}