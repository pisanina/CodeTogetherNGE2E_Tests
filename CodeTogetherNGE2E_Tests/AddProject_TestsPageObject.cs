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
            LoginUser();
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

        public void LoginUser()
        {
            _driver.FindElement(By.Id("Login")).Click();
            _driver.FindElement(By.Id("Input_Email")).SendKeys("TestUser@a.com");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("Qwedsa11!");
            _driver.FindElement(By.Id("Input_Password")).SendKeys(Keys.Enter);
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
                using (SqlCommand Delete = new SqlCommand(
                    "Delete From ProjectTechnology " +
                    "Delete From Project " +
                    "DBCC CHECKIDENT('Project', RESEED, 0) " +
                    "Declare @UId Nvarchar(450) Set @UId = '7e514a25-c4ab-4214-8414-f9ecc6dd9e9e'  " +
                    "Delete From AspNetUsers " +
                    "Insert Into AspNetUsers (Id, UserName, NormalizedUserName, " +
                    "Email, NormalizedEmail, PasswordHash, SecurityStamp, ConcurrencyStamp, EmailConfirmed,  " +
                    "PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) Values " +
                    "(@UId, 'TestUser@a.com','TESTUSER@A.COM', 'TestUser@a.com', 'TESTUSER@A.COM', " +
                    "'AQAAAAEAACcQAAAAEMjfV0AsErgwEJqu3B/qoRZupd0gee/MR+kTVqHSiw+hyQRqVkwqckRqnX8KWszcAQ==', " +
                    "'22LCTUSUQEQ3TRILLNAI4UYKWW35DMU4', '9daa59f7-3e7d-43fa-a162-0b10d51fac57', 0, 0, 0, 1, 0)"
                    +
                    "Insert Into Project(Title, Description, OwnerId, NewMembers) " +
                    "Values('FirstProject', 'Our first project will be SUPRISE Hello World', @UId, 0)," +
                    "('SecondProject', 'Yes we will print Hello Kity', @UId, 0)," +
                    "('Funny bunny', 'We want to create web aplication with many funny bunes', @UId, 1)," +
                    "(N'Polish title like pamięć', N'Test for polish worlds like pamięć', @UId, 0)," +
                    "('Test for adding Project with four Tech', 'Test for adding Project with four Technologies', @UId, 0)," +
                    "('Project with Two Tech', 'Project with Two Tech (Test)', @UId, 0) "
                    +
                    "Insert Into ProjectTechnology Values(5,2),(5, 4),(5, 6),(5, 7),(6, 6),(6, 7);"
                    , SQLConnect))
                {
                    Delete.ExecuteNonQuery();
                }
                SQLConnect.Close();
            }
        }
    }
}