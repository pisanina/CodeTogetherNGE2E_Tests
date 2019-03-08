using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace CodeTogetherNGE2E_Tests
{
    public class AddProject_Tests
    {
        private IWebDriver _driver;
        private AddProject_PageObject _add;
        private Grid_PageObject _grid;

        [TestCase("Test for adding new Project Title", "Test Description 1 of Project")]
        [TestCase("Test for adding Project Title in Polish żółtość", "Test Description 2 of Project")]
        [TestCase("Test for adding Project Title in Japanese こんにちは", "Test Description 3 of Project")]
        [TestCase("Test Title", "Test for adding Description in Polish żółtość")]
        [TestCase("Test Title", "Test for adding Description in Japanese こんにちは")]
        public void AddProjectTest(string newTitle, string newDescription)
        {
            Assert.False(_grid.IsProjectDisplayed(newTitle));
            Assert.False(_grid.IsProjectDisplayed(newDescription));

            _add.AddProject(newTitle, newDescription);
           
            Assert.True(_grid.IsProjectDisplayed(newTitle));
            Assert.True(_grid.IsProjectDisplayed(newDescription));
        }

        [TestCase("',''); CREATE LOGIN Admin WITH PASSWORD = 'ABCD'--", "Test Title for SQL Injection 1", "',''); CREATE LOGIN Admin WITH PASSWORD = 'ABCD'--", "Test Title for SQL Injection 1")]
        [TestCase("Test Description for SQL Injection", "'); CREATE LOGIN NewAdmin WITH PASSWORD = 'ABCD'--", "'); CREATE LOGIN NewAdmin WITH PASSWORD = 'ABCD'--", "Test Description for SQL Injection")]
        [TestCase("','');EXEC xp_cmdshell 'echo BUM >c:/A.txt'--'','", "Test Title for SQL Injection 2", "','');EXEC xp_cmdshell 'echo BUM &gt;c:/A.txt'--'','", "Test Title for SQL Injection 2")]
        [TestCase("Test Description for SQL Injection", "'); EXEC xp_cmdshell 'echo BUM > c:/A.txt'--'", "'); EXEC xp_cmdshell 'echo BUM &gt; c:/A.txt'--'", "Test Description for SQL Injection")]
        [TestCase("',''); <script>alert('BUM!');</script>'", "Test Title for JavaScript Injection", "',''); &lt;script&gt;alert('BUM!');&lt;/script&gt;'", "Test Title for JavaScript Injection")]
        [TestCase("Test Description for JavaScript Injection", "'); <script>alert('BUM!');</script>'--", "'); &lt;script&gt;alert('BUM!');&lt;/script&gt;'--", "Test Description for JavaScript Injection")]
        [TestCase("',''); <h1>Surprise</h1>'--'','", "Test Title for HTML Injection", "',''); &lt;h1&gt;Surprise&lt;/h1&gt;'--'','", "Test Title for HTML Injection")]
        [TestCase("Test Description for HTML Injection", "'); <h1>Surprise</h1>'--'", "'); &lt;h1&gt;Surprise&lt;/h1&gt;'--'", "Test Description for HTML Injection")]
        public void AddProjectInjectionTest(string newTitle, string newDescription, string injection, string titleORdescription)
        {
            Assert.False(_grid.IsProjectDisplayed(newTitle));
            Assert.False(_grid.IsProjectDisplayed(newDescription));

            _add.AddProject(newTitle, newDescription);

            Assert.True(_grid.IsProjectDisplayed(injection));
            Assert.True(_grid.IsProjectDisplayed(titleORdescription));
        }

        [TestCase("", "Test for adding empty Title", "Title has a minimum length of 3.")]
        [TestCase("AB", "Test for adding to short Title", "Title has a minimum length of 3.")]
        [TestCase("Test for adding empty Description", "", "Description has a minimum length of 20.")]
        [TestCase("Test for adding to short Desciption", "Short Description", "Description has a minimum length of 20.")]
        public void AddProjectTooShort(string newTitle, string newDescription, string error)
        {
            Assert.False(_add.ErrorDisplayed(error));

            _add.AddProject(newTitle, newDescription);

            Assert.True(_add.ErrorDisplayed(error));
        }

        [Test]
        public void AddProjectWithTooLongTitleAndDescription()
        {
            string tooLongTitle = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

            string tooLongDescription = new string('+', 10).Replace("+", tooLongTitle);

            Assert.False(_add.ErrorDisplayed("Title has a maximum length of 50."));
            Assert.False(_add.ErrorDisplayed("Description has a maximum length of 1000."));

            _add.AddProject(tooLongTitle, tooLongDescription);

            Assert.True(_add.ErrorDisplayed("Title has a maximum length of 50."));
            Assert.True(_add.ErrorDisplayed("Description has a maximum length of 1000."));
        }

        [Test]
        public void AddProjectThatExist()
        {
            Assert.False(_add.ErrorDisplayed("Sorry there is alredy project with that title"));

            _add.AddProject("Funny bunny", "Description for test project that exist");

            Assert.True(_add.ErrorDisplayed("Sorry there is alredy project with that title"));
        }

        [Test]
        public void AddProjectTechnology()
        {
            string newTitle = "Test77 for adding project with tech";
            string newDescription = newTitle;

            List<int> techList = new List<int>();
            techList.Add(6);
            techList.Add(7);

            _add.AddProject(newTitle, newDescription, techList, false);

            Assert.True(_grid.IsProjectDisplayed(newTitle));
            Assert.True(_grid.IsProjectDisplayed("Java, JavaScript"));
        }

        [Test]
        public void AddProjectWithNewMembers()
        {
            string newTitle = "Test77 for adding project with tech";
            string newDescription = newTitle;

            List<int> techList = new List<int>();
            techList.Add(6);
            techList.Add(7);

            _add.AddProject(newTitle, newDescription, techList, true);

            Assert.True(_grid.IsProjectDisplayed(newTitle));
            Assert.True(_grid.IsNewMembersDisplayed(7));
            Assert.True(_grid.IsProjectDisplayed("Java, JavaScript"));
        }

        [SetUp]
        public void SeleniumSetup()
        {
            _driver = new ChromeDriver(Configuration.WebDriverLocation);
            _driver.Url = Configuration.WebApiUrl;
            _add = new AddProject_PageObject(_driver);
            _grid = new Grid_PageObject(_driver);
            _add.PrepareDB();
            _add.ClickCookieConsent();
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}