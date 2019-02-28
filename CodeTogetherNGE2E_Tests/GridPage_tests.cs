using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            string toSearch = "We want to create web aplication with many funny bunes";
            string toSearchFirst50Chars = "We want to create web aplication with many funny";

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
            Assert.True(_driver.PageSource.Contains("Project with Two Tech")); //
            searchInput.Clear();

            //Check for proper Search behavior when two letters are enter
            searchInput.SendKeys("Fu");
            searchInput.SendKeys(Keys.Enter);
            Assert.False(_driver.PageSource.Contains("Project with Two Tech"));
            Assert.True(_driver.PageSource.Contains("Funny"));
        }

        [Test]
        public void SearchProjectWithTechnologies()
        {
            string toSearch = "Project";
            List<int> techIdList = new List<int>();
            techIdList.Add(4);
            techIdList.Add(6);
            techIdList.Add(7);

            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.True(_driver.PageSource.Contains("Funny"));
            Assert.True(_driver.PageSource.Contains(toSearch, System.StringComparison.InvariantCultureIgnoreCase));

            _driver.FindElement(By.Id("SearchInput")).SendKeys(toSearch);

            var TechList = _driver.FindElement(By.Id("TechList"));

            foreach (var item in techIdList)
            {
                TechList.FindElement(By.CssSelector("option[value=\"" + item + "\"]")).Click();
            }

            _driver.FindElement(By.Id("SearchButton")).Click();

            var projectsList = _driver.FindElements(By.XPath("/html/body/div/div/div/a/div/small"));
            Assert.False(_driver.PageSource.Contains("Funny"));
            Assert.True(projectsList.Count == 1);
            Assert.True(_driver.PageSource.Contains(toSearch, System.StringComparison.InvariantCultureIgnoreCase));
            Assert.True(searchTech(projectsList, "Assembly, C++, Java, JavaScript"));
        }

        [Test]
        public void DetailView()
        {
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.True(_driver.PageSource.Contains("Search"));

            _driver.FindElement(By.XPath("/html/body/div/div/div[1]/a")).Click();

            Assert.False(_driver.PageSource.Contains("Search"));
            Assert.True(_driver.PageSource.Contains("CreationDate"));
            Assert.True(_driver.PageSource.Contains("TestUser@a.com"));
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
            Assert.True(_driver.PageSource.Contains("TestUser@a.com"));
            Assert.True(_driver.PageSource.Contains("Funny"));
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
            var  Temp = _driver.FindElement(By.CssSelector("option[value='3']"));

            Assert.False(_driver.PageSource.Contains("Search"));
            Assert.True(_driver.PageSource.Contains("Project with Two Tech (Test)"));
            Assert.True(_driver.PageSource.Contains("value=\"6\" selected=\"\">Java"));
            Assert.True(_driver.PageSource.Contains("value=\"7\" selected=\"\">JavaScript"));
            Assert.True(_driver.PageSource.Contains("<option value=\"4\">C++</option>"));
            Assert.True(_driver.PageSource.Contains("TestUser@a.com"));
            Assert.True(Temp.Selected == false);
        }

        [Test]
        public void GridViewTechnology()
        {
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            var projectsList = _driver.FindElements(By.XPath("/html/body/div/div/div/a/div/small"));

            Assert.True(searchTech(projectsList, "Java, JavaScript"));
            Assert.True(_driver.PageSource.Contains("Project with Two Tech"));
            Assert.True(_driver.PageSource.Contains("Java, JavaScript"));
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

        public bool searchTech(ReadOnlyCollection<IWebElement> project, string Technologies)
        {
            bool found = false;
            foreach (var item in project)
            {
                if (item.Text == Technologies)
                { found = true; break; }
                else
                { found = false; }
            }
            return found;
        }
    }
}