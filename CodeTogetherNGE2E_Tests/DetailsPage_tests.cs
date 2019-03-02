using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace CodeTogetherNGE2E_Tests
{
    internal class DetailsPage_Tests
    {
        private IWebDriver _driver;
        private Details_PageObject _details;
        private Grid_PageObject _grid;

        [Test]
        public void RedirectToDetailView()
        {
            _grid.ClickTheFirstProject();
            Assert.True(_details.IsOnDetailsView());
        }

        [Test]
        public void DetailView_EnsureProjectValuesAreCorrect()
        {
            _grid.Search("Funny");

            _grid.ClickTheFirstProject();

            Assert.True(_details.GetTitle() == "Funny bunny");
            Assert.True(_details.GetOwner() == "TestUser@a.com");
            Assert.True(_details.GetDescription() == "We want to create web aplication with many funny bunes");
            Assert.True(_details.GetCreationDate() == DateTime.Now.ToString("dd/MM/yyy"));
            Assert.True(_details.GetNewMembers());
        }

        [Test]
        public void DetailViewTechnology()
        {
            _grid.Search("Two Tech (Test)");

            _grid.ClickTheFirstProject();

            Assert.True(_details.IsTechnologySelected(6));
            Assert.True(_details.IsTechnologySelected(7));
            Assert.False(_details.IsTechnologySelected(1));
            Assert.False(_details.IsTechnologySelected(2));
            Assert.False(_details.IsTechnologySelected(3));
            Assert.False(_details.IsTechnologySelected(4));
            Assert.False(_details.IsTechnologySelected(5));
            Assert.False(_details.IsTechnologySelected(8));
            Assert.False(_details.IsTechnologySelected(9));
            Assert.False(_details.IsTechnologySelected(10));
            Assert.False(_details.IsTechnologySelected(11));
            Assert.False(_details.IsTechnologySelected(13));
            Assert.False(_details.IsTechnologySelected(14));
            Assert.False(_details.IsTechnologySelected(15));
            Assert.False(_details.IsTechnologySelected(16));
            Assert.False(_details.IsTechnologySelected(17));
            Assert.False(_details.IsTechnologySelected(18));
        }

        [SetUp]
        public void SeleniumSetup()
        {
            AddProject_TestsPageObject.PrepareDB();

            _driver = new ChromeDriver(Configuration.WebDriverLocation);
            _driver.Url = Configuration.WebApiUrl;

            _details = new Details_PageObject(_driver);

            _grid = new Grid_PageObject(_driver);

            _driver.FindElement(By.XPath("//*[@id=\"cookieConsent\"]/div/div[2]/div/button")).Click();
            _driver.FindElement(By.Id("ProjectsGrid")).Click();
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}