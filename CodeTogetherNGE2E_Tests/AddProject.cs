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

        [Test]
        public void AddProjectTest()
        {
            string NewTitle       = "Test for adding new Project Title";
            string NewDescription = "Test for adding new Project Description";

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