﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CodeTogetherNGE2E_Tests
{
    internal class GridPage_tests
    {
        private IWebDriver _driver;

        [TestCase("Project")] // Search in Title
        [TestCase("Yes")]  // Search in Description
        [TestCase("ęć")]   // Test for foreign languages
        [TestCase("TEST")] // Test for key sensitive in search
        public void SearchProject(string ToSearch)
        {
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.True(_driver.PageSource.Contains("Funny"));
            Assert.True(_driver.PageSource.Contains(ToSearch, System.StringComparison.InvariantCultureIgnoreCase));

            _driver.FindElement(By.Id("SearchInput")).SendKeys(ToSearch);
            _driver.FindElement(By.Id("SearchButton")).Click();

            Assert.False(_driver.PageSource.Contains("Funny"));
            Assert.True(_driver.PageSource.Contains(ToSearch, System.StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void SearchProjectToolong()
        {
            string toSearch = "We create this project to test our search input, especially it's lenght";
            string toSearchFirst50Chars = "We create this project to test our search input, e";

            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            var searchInput = _driver.FindElement(By.Id("SearchInput"));

            searchInput.SendKeys(toSearch);

            // Assert that input Search cut first 50 chars of toSearch
            Assert.False(_driver.PageSource.Contains(toSearch, System.StringComparison.InvariantCultureIgnoreCase));
            searchInput.SendKeys(Keys.Enter);

            Assert.True(_driver.PageSource.Contains(toSearchFirst50Chars, System.StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void SearchProjectMinLenght()
        {
            _driver.FindElement(By.Id("ProjectsGrid")).Click();
            Assert.True(_driver.PageSource.Contains("Test", System.StringComparison.InvariantCultureIgnoreCase));

            var searchInput = _driver.FindElement(By.Id("SearchInput"));

            //Check if SearchInput has validation
            Assert.IsNotEmpty(searchInput.GetAttribute("validationMessage"));

            searchInput.SendKeys("F");
            searchInput.SendKeys(Keys.Enter);
            //Check for Project in grid that don't have leter "F" in Title or Description
            //so we now that Search didn't run
            Assert.True(_driver.PageSource.Contains("Another simply Test")); //
            searchInput.Clear();

            //Check for proper Search behavior when two letters are enter
            searchInput.SendKeys("Fu");
            searchInput.SendKeys(Keys.Enter);
            Assert.False(_driver.PageSource.Contains("Another simply Test"));
            Assert.True(_driver.PageSource.Contains("Funny"));
        }

        [Test]
        public void DetailView()
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