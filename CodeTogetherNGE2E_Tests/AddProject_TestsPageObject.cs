using OpenQA.Selenium;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodeTogetherNGE2E_Tests
{
    public class AddProject_TestsPageObject
    {
        private IWebDriver _driver;

        public AddProject_TestsPageObject(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void AddProject(string title, string description, IEnumerable<int> techIdList)
        {
            _driver.FindElement(By.Id("AddProject")).Click();
            _driver.FindElement(By.Id("Title")).SendKeys(title);
            _driver.FindElement(By.Id("Description")).SendKeys(description);
            var TechList = _driver.FindElement(By.Id("TechList"));

            foreach (var item in techIdList)
            {
                TechList.FindElement(By.CssSelector("option[value=\"" + item + "\"]")).Click();
            }

            _driver.FindElement(By.Id("Create")).Click();
        }

        public void AddProject(string title, string description)
        {
            IEnumerable<int> techIdList = new List<int>();
            AddProject(title, description, techIdList);
        }

        public static void PrepareDB()
        {
            using (SqlConnection SQLConnect =
                new SqlConnection(Configuration.ConnectionString))
            {
                SQLConnect.Open();
                using (SqlCommand Delete = new SqlCommand("Delete From ProjectTechnology Where ProjectId > 0 " +
                    "Delete From Project Where ID > 0 DBCC CHECKIDENT('Project', RESEED, 0) " +
                    "Insert Into Project(Title, Description) " +
                    "Values('FirstProject', 'Our first project will be SUPRISE Hello World')," +
                    "('SecondProject', 'Yes we will print Hello Kity')," +
                    "('Funny bunny', 'We want to create web aplication with many funny bunes')," +
                    "(N'Polish title like pamięć', N'Test for polish worlds like pamięć')," +
                    "('Test for adding Project with four Tech', 'Test for adding Project with four Technologies')," +
                    "('Project with Two Tech', 'Project with Two Tech (Test)') " +
                    "Insert Into ProjectTechnology Values(5,2),(5, 4),(5, 6),(5, 7),(6, 6),(6, 7);", SQLConnect))
                {
                    Delete.ExecuteNonQuery();
                }
                SQLConnect.Close();
            }
        }
    }
}