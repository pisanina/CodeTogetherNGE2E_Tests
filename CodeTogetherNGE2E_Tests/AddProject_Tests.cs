﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace CodeTogetherNGE2E_Tests
{
    public class AddProject_Tests
    {
        private IWebDriver _driver;
        private AddProject_TestsPageObject _add;

        [TestCase("Test for adding new Project Title", "Test Description 1 of Project")]
        [TestCase("Test for adding Project Title in Polish żółtość", "Test Description 2 of Project")]
        [TestCase("Test for adding Project Title in Japanese こんにちは", "Test Description 3 of Project")]
        [TestCase("Test Title", "Test for adding Description in Polish żółtość")]
        [TestCase("Test Title", "Test for adding Description in Japanese こんにちは")]
        public void AddProjectTest(string NewTitle, string NewDescription)
        {
            Assert.False(_driver.PageSource.Contains(NewTitle));
            Assert.False(_driver.PageSource.Contains(NewDescription));

            _add.AddProject(NewTitle, NewDescription);

            Assert.True(_driver.PageSource.Contains(NewTitle));
            Assert.True(_driver.PageSource.Contains(NewDescription));
        }

        [TestCase("',''); CREATE LOGIN Admin WITH PASSWORD = 'ABCD'--", "Test Title for SQL Injection 1", "',''); CREATE LOGIN Admin WITH PASSWORD = 'ABCD'--", "Test Title for SQL Injection 1")]
        [TestCase("Test Description for SQL Injection", "'); CREATE LOGIN NewAdmin WITH PASSWORD = 'ABCD'--", "'); CREATE LOGIN NewAdmin WITH PASSWORD = 'ABCD'--", "Test Description for SQL Injection")]
        [TestCase("','');EXEC xp_cmdshell 'echo BUM >c:/A.txt'--'','", "Test Title for SQL Injection 2", "','');EXEC xp_cmdshell 'echo BUM &gt;c:/A.txt'--'','", "Test Title for SQL Injection 2")]
        [TestCase("Test Description for SQL Injection", "'); EXEC xp_cmdshell 'echo BUM > c:/A.txt'--'", "'); EXEC xp_cmdshell 'echo BUM &gt; c:/A.txt'--'", "Test Description for SQL Injection")]
        [TestCase("',''); <script>alert('BUM!');</script>'", "Test Title for JavaScript Injection", "',''); &lt;script&gt;alert('BUM!');&lt;/script&gt;'", "Test Title for JavaScript Injection")]
        [TestCase("Test Description for JavaScript Injection", "'); <script>alert('BUM!');</script>'--", "'); &lt;script&gt;alert('BUM!');&lt;/script&gt;'--", "Test Description for JavaScript Injection")]
        [TestCase("',''); <h1>Surprise</h1>'--'','", "Test Title for HTML Injection", "',''); &lt;h1&gt;Surprise&lt;/h1&gt;'--'','", "Test Title for HTML Injection")]
        [TestCase("Test Description for HTML Injection", "'); <h1>Surprise</h1>'--'", "'); &lt;h1&gt;Surprise&lt;/h1&gt;'--'", "Test Description for HTML Injection")]
        public void AddProjectInjectionTest(string NewTitle, string NewDescription, string Injection, string TitleORdescription)
        {
            Assert.False(_driver.PageSource.Contains(NewTitle));
            Assert.False(_driver.PageSource.Contains(NewDescription));

            _add.AddProject(NewTitle, NewDescription);

            Assert.True(_driver.PageSource.Contains(Injection));
            Assert.True(_driver.PageSource.Contains(TitleORdescription));
        }

        [TestCase("", "Test for adding empty Title", "Title has a minimum length of 3.")]
        [TestCase("AB", "Test for adding to short Title", "Title has a minimum length of 3.")]
        [TestCase("Test for adding empty Description", "", "Description has a minimum length of 20.")]
        [TestCase("Test for adding to short Desciption", "Short Description", "Description has a minimum length of 20.")]
        public void AddProjectTooShort(string NewTitle, string NewDescription, string Error)
        {
            Assert.False(_driver.PageSource.Contains(Error));

            _add.AddProject(NewTitle, NewDescription);

            Assert.True(_driver.PageSource.Contains(Error));
        }

        [Test]
        public void AddProjectWithTooLongTitleAndDescription()
        {
            string TooLongTitle = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

            string TooLongDescription = new string('+', 10).Replace("+", TooLongTitle);

            Assert.False(_driver.PageSource.Contains("Title has a maximum length of 50."));
            Assert.False(_driver.PageSource.Contains("Description has a maximum length of 1000."));

            _add.AddProject(TooLongTitle, TooLongDescription);

            Assert.True(_driver.PageSource.Contains("Title has a maximum length of 50."));
            Assert.True(_driver.PageSource.Contains("Description has a maximum length of 1000."));
        }

        [Test]
        public void AddProjectThatExist()
        {
            Assert.False(_driver.PageSource.Contains("Sorry there is alredy project with that title"));

            _add.AddProject("Funny bunny", "Description for test project that exist");

            Assert.True(_driver.PageSource.Contains("Sorry there is alredy project with that title"));
        }

        [Test]
        public void AddProjectTechnology()
        {
            string newTitle = "Test77 for adding project with tech";
            string newDescription = newTitle;

            List<int> techList = new List<int>();
            techList.Add(6);
            techList.Add(7);

            _add.AddProject(newTitle, newDescription, techList);

            Assert.True(_driver.PageSource.Contains(newTitle));
            Assert.True(_driver.PageSource.Contains("Java, JavaScript"));
        }

        [SetUp]
        public void SeleniumSetup()
        {
            _driver = new ChromeDriver(Configuration.WebDriverLocation);
            _driver.Url = Configuration.WebApiUrl;
            _add = new AddProject_TestsPageObject(_driver);
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