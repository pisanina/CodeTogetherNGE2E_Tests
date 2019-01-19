using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace CodeTogetherNGE2E_Tests
{
    public class AddProject_Tests
    {
        private IWebDriver _driver;
        private AddProject_TestsPageObject Add;

        [TestCase("Test for adding new Project Title", "Test Description of Project")]
        [TestCase("Test for adding Project Title in Polish żółtość", "Test Description of Project")]
        [TestCase("Test for adding Project Title in Japanese こんにちは", "Test Description of Project")]
        [TestCase("Test Title", "Test for adding Description in Polish żółtość")]
        [TestCase("Test Title", "Test for adding Description in Japanese こんにちは")]
        public void AddProjectTest(string NewTitle, string NewDescription)
        {
            Add.AddProject(NewTitle, NewDescription);

            Assert.True(_driver.PageSource.Contains(NewTitle));
            Assert.True(_driver.PageSource.Contains(NewDescription));

            SqlDelete(NewTitle);
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.False(_driver.PageSource.Contains(NewTitle));
            Assert.False(_driver.PageSource.Contains(NewDescription));
        }

        [TestCase("',''); CREATE LOGIN Admin WITH PASSWORD = 'ABCD'--", "Test Title for SQL Injection", "',''); CREATE LOGIN Admin WITH PASSWORD = 'ABCD'--", "Test Title for SQL Injection")]
        [TestCase("Test Description for SQL Injection", "'); CREATE LOGIN NewAdmin WITH PASSWORD = 'ABCD'--", "'); CREATE LOGIN NewAdmin WITH PASSWORD = 'ABCD'--", "Test Description for SQL Injection")]
        [TestCase("','');EXEC xp_cmdshell 'echo BUM >c:/A.txt'--'','", "Test Title for SQL Injection", "','');EXEC xp_cmdshell 'echo BUM &gt;c:/A.txt'--'','", "Test Title for SQL Injection")]
        [TestCase("Test Description for SQL Injection", "'); EXEC xp_cmdshell 'echo BUM > c:/A.txt'--'", "'); EXEC xp_cmdshell 'echo BUM &gt; c:/A.txt'--'", "Test Description for SQL Injection")]

        [TestCase("',''); <script>alert('BUM!');</script>'", "Test Title for JavaScript Injection", "',''); &lt;script&gt;alert('BUM!');&lt;/script&gt;'", "Test Title for JavaScript Injection")]
        [TestCase("Test Description for JavaScript Injection", "'); <script>alert('BUM!');</script>'--", "'); &lt;script&gt;alert('BUM!');&lt;/script&gt;'--", "Test Description for JavaScript Injection")]

        [TestCase("',''); <h1>Surprise</h1>'--'','", "Test Title for HTML Injection", "',''); &lt;h1&gt;Surprise&lt;/h1&gt;'--'','", "Test Title for HTML Injection")]
        [TestCase("Test Description for HTML Injection", "'); <h1>Surprise</h1>'--'", "'); &lt;h1&gt;Surprise&lt;/h1&gt;'--'", "Test Description for HTML Injection")]
        public void AddProjectInjectionTest(string NewTitle, string NewDescription, string Injection, string TitleORdescription)
        {
            Add.AddProject(NewTitle, NewDescription);

            Assert.True(_driver.PageSource.Contains(Injection));
            Assert.True(_driver.PageSource.Contains(TitleORdescription));

            SqlDelete(NewTitle);
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.False(_driver.PageSource.Contains(Injection));
            Assert.False(_driver.PageSource.Contains(TitleORdescription));
        }

        [TestCase("", "Test for adding empty Title", "Title has a minimum length of 3.")]
        [TestCase("AB", "Test for adding to short Title", "Title has a minimum length of 3.")]
        [TestCase("Test for adding empty Description", "", "Description has a minimum length of 20.")]
        [TestCase("Test for adding to short Desciption", "Short Description", "Description has a minimum length of 20.")]
        public void AddProjectTooShort(string NewTitle, string NewDescription, string Error)
        {
            Assert.False(_driver.PageSource.Contains(Error));

            Add.AddProject(NewTitle, NewDescription);

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

            Add.AddProject(TooLongTitle, TooLongDescription);

            Assert.True(_driver.PageSource.Contains("Title has a maximum length of 50."));
            Assert.True(_driver.PageSource.Contains("Description has a maximum length of 1000."));
        }

        private void SqlDelete(string ToDelete)
        {
            using (SqlConnection SQLConnect =
                new SqlConnection("Server=DESKTOP-67FEEF1\\SQLEXPRESS;Database=CodeTogetherNG;User Id=codetogetherng;Password=#EDC2wsx$RFV;"))
            {
                SQLConnect.Open();
                using (SqlCommand Delete = new SqlCommand("Delete from Project Where Title = @T;", SQLConnect))
                {
                    Delete.Parameters.Add(new SqlParameter("T", ToDelete));
                    Delete.ExecuteNonQuery();
                }
                SQLConnect.Close();
            }
        }

        [SetUp]
        public void SeleniumSetup()
        {
            var webDriverLocation = TestContext.Parameters["webDriverLocation"];
            if (string.IsNullOrWhiteSpace(webDriverLocation))
                webDriverLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
           _driver = new ChromeDriver(webDriverLocation);

            _driver.Url = TestContext.Parameters["webAppUrl"];

            Add = new AddProject_TestsPageObject(_driver);

            _driver.FindElement(By.XPath("//*[@id=\"cookieConsent\"]/div/div[2]/div/button")).Click();
        }

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}