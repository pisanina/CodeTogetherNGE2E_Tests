using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace CodeTogetherNGE2E_Tests
{
    [TestFixture]
    internal class DetailsPage_Tests
    {
        private IWebDriver _driver;
        private Details_PageObject _details;
        private Grid_PageObject _grid;
        private Requests_PageObject _request;

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

        [Test]
        public void EditDetailView()
        {
            _grid.LoginOwner();
            _grid.GoToProjectsGrid();
            _grid.ClickTheFirstProject();
            Assert.True(_details.GetTitle() == "FirstProject");
            Assert.False(_details.IsOwnerNameEditable());
            Assert.True(_details.GetDescription() == "Our first project will be SUPRISE Hello World");
            Assert.False(_details.IsCreationDateEditable());
            Assert.True(_details.GetNewMembers() == false);
            Assert.True(_details.GetProjectState() == 2);
            Assert.True(_details.GetSelectedTechnologies().Count == 0);

            _details.EditTitle("FirstProject2");
            _details.EditDescription("Our first project will be SUPRISE Hello World - changed");
            _details.EditNewMembers(true);
            _details.SelectTechnology(1);
            _details.SelectProjectState(1);

            _details.EditSave();

            Assert.True(_grid.IsOnPage_ProjectsGrid());
            _grid.ClickTheFirstProject();

            Assert.True(_details.GetTitle() == "FirstProject2");
            Assert.True(_details.GetDescription() == "Our first project will be SUPRISE Hello World - changed");
            Assert.True(_details.GetNewMembers() == true);
            Assert.True(_details.GetProjectState() == 1);
            Assert.True(_details.GetSelectedTechnologies().Count == 1);
        }

        [TestCase("true")]
        [TestCase("false")]
        public void EditDetailViewByNotOwner(string logged)
        {
            if (logged == "true") { _grid.LoginCoder(); }

            _grid.GoToProjectsGrid();
            _grid.ClickTheFirstProject();
            Assert.False(_details.IsTitleEditable());
            Assert.False(_details.IsOwnerNameEditable());
            Assert.False(_details.IsDescriptionEditable());
            Assert.False(_details.IsCreationDateEditable());
            Assert.False(_details.IsNewMembersEditable());
            Assert.False(_details.IsTechnologyListEditable());
            Assert.False(_details.IsProjectStateEditable());
            Assert.False(_details.IsSaveButtonOnPage());
        }

        [Test]
        public void EditDetailsWrongDataInput()
        {
            _grid.LoginOwner();
            _grid.GoToProjectsGrid();
            _grid.ClickTheFirstProject();

            Assert.True(_details.GetTitle() == "FirstProject");
            Assert.True(_details.GetDescription() == "Our first project will be SUPRISE Hello World");

            string title = "";
            string description = "";
            string error = "Please fill this field";

            _details.EditTitle(title);
            _details.EditSave();
            _details.ErrorDisplayed(error);

            _details.EditDescription(description);
            _details.EditSave();
            _details.ErrorDisplayed(error);

            title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

            description = new string('+', 10).Replace("+", title);

            _details.EditTitle(title);
            _details.EditSave();
            _details.ErrorDisplayed(error);

            _details.EditDescription(description);
            _details.EditSave();
            _details.ErrorDisplayed(error);
        }

        [Test]
        public void RequestAccepted()
        {
            _grid.LoginCoder();
            _grid.GoToProjectsGrid();
            _grid.ClickTheSecondProject();
            _details.EditMessage("I want to join");
            _details.Logout();
            _grid.LoginOwner();
            _grid.GoToProjectsGrid();
            _grid.ClickTheSecondProject();
            _details.ClickShowRequestsButton();
            Assert.True(_request.CheckMessage(2, "I want to join"));
            _request.AcceptButtonClick();
            _request.GoToProjectsGrid();
            _grid.ClickTheSecondProject();
            Assert.True(_details.GetMembers().Trim() == "coder@a.com");
            Assert.False(_details.IsShowRequestsButtonOnPage());
            _details.Logout();

            _grid.LoginCoder();
            _grid.GoToProjectsGrid();
            _grid.ClickTheSecondProject();
            Assert.False(_details.IsSendButtonOnPage());
        }

        [Test]
        public void RequestDeclined()
        {
            _grid.LoginCoder();
            _grid.GoToProjectsGrid();
            _grid.ClickTheFirstProject();
            _details.EditMessage("I want to join");
            _details.Logout();
            _grid.LoginOwner();
            _grid.GoToProjectsGrid();
            _grid.ClickTheFirstProject();
            _details.ClickShowRequestsButton();
            Assert.True(_request.CheckMessage(1, "I want to join"));
            _request.DeclineButtonClick();
            _request.GoToProjectsGrid();
            _grid.ClickTheFirstProject();
            Assert.False(_details.GetMembers().Trim() == "coder@a.com");
            Assert.False(_details.IsShowRequestsButtonOnPage());
            _details.Logout();

            _grid.LoginCoder();
            _grid.GoToProjectsGrid();
            _grid.ClickTheFirstProject();
            Assert.True(_details.GetReqestStatusMessage().Contains("Your unable to send a join request until"));
        }

        [Test]
        public void ReqestIsPendingMessage()
        {
            _grid.LoginCoder();
            _grid.GoToProjectsGrid();
            _grid.ClickTheFirstProject();
            _details.EditMessage("I want to join");
            _grid.ClickTheFirstProject();

            Assert.True(_details.GetReqestStatusMessage() == "Your request is pending");
        }

        [Test]
        [SetUp]
        public void SeleniumSetup()
        {
            _driver = new ChromeDriver(Configuration.WebDriverLocation);
            _driver.Url = Configuration.WebApiUrl;

            _details = new Details_PageObject(_driver);

            _grid = new Grid_PageObject(_driver);
            _grid.PrepareDB();

            _request = new Requests_PageObject(_driver);

            _grid.ClickCookieConsent();
            _grid.GoToProjectsGrid();
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}