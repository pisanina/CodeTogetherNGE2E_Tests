using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace CodeTogetherNGE2E_Tests
{
    public class AddProject
    {
        private IWebDriver _driver;

        [SetUp]
        public void SeleniumSetup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Url = "https://localhost:44362/";
            // _driver.Url = "https://codetogetherng.azurewebsites.net/";
            _driver.FindElement(By.XPath("//*[@id=\"cookieConsent\"]/div/div[2]/div/button")).Click();
        }

        [TestCase("Test for adding new Project Title", "Test Description of Project")]
        [TestCase("Test for adding Project Title in Polish żółtość", "Test Description of Project")]
        [TestCase("Test for adding Project Title in Japanese こんにちは", "Test Description of Project")]
        [TestCase("Test Title", "Test for adding Description in Polish żółtość")]
        [TestCase("Test Title", "Test for adding Description in Japanese こんにちは")]
        // Tests for SQL Injection
        [TestCase("',''); CREATE LOGIN Admin WITH PASSWORD = 'ABCD'--", "Test Title for SQL Injection")]
        [TestCase("Test Description for SQL Injection", "'); CREATE LOGIN NewAdmin WITH PASSWORD = 'ABCD'--")]

        public void AddProjectTest(string NewTitle, string NewDescription)
        {
            _driver.FindElement(By.Id("AddProject")).Click();

            _driver.FindElement(By.Id("Title")).SendKeys(NewTitle);

            _driver.FindElement(By.Id("Description")).SendKeys(NewDescription);

            _driver.FindElement(By.Id("Create")).Click();

            Assert.True(_driver.PageSource.Contains(NewTitle));
            Assert.True(_driver.PageSource.Contains(NewDescription));

            SqlDelete(NewTitle);
            _driver.FindElement(By.Id("ProjectsGrid")).Click();

            Assert.False(_driver.PageSource.Contains(NewTitle));
            Assert.False(_driver.PageSource.Contains(NewDescription));
        }

        [TestCase("", "Test for adding empty Title", "Title-error")]
        [TestCase("AB", "Test for adding to short Title", "Title-error")]
        [TestCase("Test for adding empty Description", "", "Description-error")]
        [TestCase("Test for adding to short Desciption", "Short Description", "Description-error")]
        public void AddProjectToShort(string NewTitle, string NewDescription, string Error)
        {
            _driver.FindElement(By.Id("AddProject")).Click();

            _driver.FindElement(By.Id("Title")).SendKeys(NewTitle);

            _driver.FindElement(By.Id("Description")).SendKeys(NewDescription);

            _driver.FindElement(By.Id("Create")).Click();

            Assert.NotNull(_driver.FindElement(By.Id(Error)));
        }

        [Test]
        public void AddProjectWithTooLongTitleAndDescription()
        {
            string TooLongTitle = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

            string TooLongDescription = new string('+', 10).Replace("+", TooLongTitle);

            _driver.FindElement(By.Id("AddProject")).Click();

            _driver.FindElement(By.Id("Title")).SendKeys(TooLongTitle);

            _driver.FindElement(By.Id("Description")).SendKeys(TooLongDescription);

            _driver.FindElement(By.Id("Description")).SendKeys(Keys.Tab);

            Assert.NotNull(_driver.PageSource.Contains("Title has a maximum length of 50."));
            Assert.NotNull(_driver.PageSource.Contains("Description has a maximum length of 1000."));
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

        [TearDown]
        public void DownSelenium()
        {
            _driver.Quit();
        }
    }
}