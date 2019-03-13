using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeTogetherNGE2E_Tests
{
    [TestFixture]
    internal class GridPage_Tests
    {
        private IWebDriver _driver;
        private Grid_PageObject _grid;

        [TestCase("Project")] // Search in Title
        [TestCase("Yes")]  // Search in Description
        [TestCase("ęć")]   // Test for foreign languages
        [TestCase("TEST")] // Test for key sensitive in search
        public void SearchProject(string toSearch)
        {
            Assert.True(_grid.IsProjectDisplayed("Funny"));
            Assert.True(_grid.IsProjectDisplayed(toSearch));

            _grid.Search(toSearch);

            Assert.False(_grid.IsProjectDisplayed("Funny"));
            Assert.True(_grid.IsProjectDisplayed(toSearch));
        }

        [TestCase("',''); CREATE LOGIN Admin WITH PASSWORD = 'ABCD'--")] 
        [TestCase("','');EXEC xp_cmdshell 'echo BUM >c:/A.txt'--'','")]  
        [TestCase("',''); <script>alert('BUM!');</script>'")]   
        [TestCase("',''); <h1>Surprise</h1>'--'','")] 
        public void SearchProjectInjectionTest(string toSearch)
        {
            Assert.True(_grid.IsProjectDisplayed("Funny"));
           
            _grid.Search(toSearch);

            Assert.True(_grid.IsOnEmptyProjectListAfterSearch());
        }


        [Test]
        public void SearchProjectToolong()
        {
            string toSearch = "We want to create web aplication with many funny bunes";
            string toSearchFirst50Chars = "We want to create web aplication with many funny";

            _grid.Search(toSearch, false);

            Assert.False(_grid.GetSearch() == toSearch);
            _grid.SearchSubmit();

            Assert.True(_grid.IsProjectDisplayed(toSearchFirst50Chars));
        }

        [Test]
        public void SearchProjectMinLenght()
        {
            Assert.True(_grid.IsProjectDisplayed("Test"));

            _grid.IsSearchInputRequired();
            _grid.Search("F");

            Assert.True(_grid.IsProjectDisplayed("Project with Two Tech")); //

            _grid.Search("Fu");
            Assert.False(_grid.IsProjectDisplayed("Project with Two Tech"));
            Assert.True(_grid.IsProjectDisplayed("Funny"));
        }

        [Test]
        public void SearchProjectMinLenghtWithOtherParameters()
        {
            _grid.SelectNewMembers("true");
            _grid.Search("");
            Assert.True(_grid.GetProjectCount() == 1);
            Assert.True(_grid.IsProjectDisplayed("Funny bunny"));
        }

        [Test]
        public void SearchProjectWithTechnologies()
        {
            string toSearch = "Project";
            List<int> techIdList = new List<int>();
            techIdList.Add(4);
            techIdList.Add(6);
            techIdList.Add(7);

            Assert.True(_grid.IsProjectDisplayed("Funny"));
            Assert.True(_grid.IsProjectDisplayed(toSearch));

            foreach (var item in techIdList)
            {
                _grid.SelectTechnology(item);
            }

            _grid.Search(toSearch);

            Assert.True(_grid.GetProjectCount() == 1);
            Assert.True(_grid.IsProjectDisplayed(toSearch));
            Assert.True(_grid.IsTechnologiesDisplayed(5, "Assembly, C++, Java, JavaScript"));
        }

        [Test]
        public void GridViewTechnology()
        {
            Assert.True(_grid.IsProjectDisplayed("Project with Two Tech"));
            Assert.True(_grid.IsTechnologiesDisplayed(6, "Java, JavaScript"));
        }

        [Test]
        public void SerchOnlyByProjectState()
        {
            _grid.SelectProjectState(2);
            _grid.SearchSubmit();
            Assert.True(_grid.GetProjectCount() == 1);
            Assert.True(_grid.IsProjectDisplayed("FirstProject"));
        }

        [Test]
        public void SerchOnlyByNewMembers()
        {
            _grid.SelectNewMembers("true");
            _grid.SearchSubmit();
            Assert.True(_grid.GetProjectCount() == 1);
            Assert.True(_grid.IsProjectDisplayed("Funny bunny"));
        }

        [Test]
        public void SearchProjectThatNoExist()
        {
            _grid.Search("Project that not exist");
            Assert.True(_grid.IsOnEmptyProjectListAfterSearch());
            _grid.ClickButtonPleaseTryAgain();
            Assert.True(_grid.IsOnPage_ProjectsGrid());
        }

        [SetUp]
        public void SeleniumSetup()
        {
            _driver = new ChromeDriver(Configuration.WebDriverLocation);
            _driver.Url = Configuration.WebApiUrl;
            _grid = new Grid_PageObject(_driver);
            _grid.PrepareDB();
            _grid.ClickCookieConsent();
            _grid.GoToProjectsGrid();
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