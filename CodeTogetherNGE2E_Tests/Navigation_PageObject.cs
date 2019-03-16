using OpenQA.Selenium;
using System.Data.SqlClient;

namespace CodeTogetherNGE2E_Tests
{
    public class Navigation_PageObject
    {
        protected IWebDriver _driver;

        public Navigation_PageObject(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void GoToHome()
        {
            _driver.FindElement(By.Id("Home")).Click();
        }

        public void GoToAbout()
        {
            _driver.FindElement(By.Id("About")).Click();
        }

        public void GoToContact()
        {
            _driver.FindElement(By.Id("Contact")).Click();
        }

        public void GoToAddProject()
        {
            _driver.FindElement(By.Id("AddProject")).Click();
        }

        public void GoToProjectsGrid()
        {
            _driver.FindElement(By.Id("ProjectsGrid")).Click();
        }

        public void GoToTechnologyList()
        {
            _driver.FindElement(By.Id("TechnologiesList")).Click();
        }

        public void GoToRegister()
        {
            _driver.FindElement(By.Id("Register")).Click();
        }

        public void GoToLogin()
        {
            _driver.FindElement(By.Id("Login")).Click();
        }

        public string GetTextfromElement(string element)
        {
            return _driver.FindElement(By.Id("element")).Text;
        }

        public bool IsOnPage_Home()
        {
            return _driver.FindElement(By.Id("HomeTitle")).Text == ("This side is under construction.");
        }

        public bool IsOnPage_About()
        {
            return _driver.FindElement(By.Id("AboutPage")).Text == ("CodeTogetherNG is a project that connects programers who want to learn how to develop applications in teams.");
        }

        public bool IsOnPage_Contact()
        {
            return _driver.FindElement(By.Id("ContactPage")).Text == ("Wannabe a developer");
        }

        public bool IsOnPage_ProjectsGrid()
        {
            return _driver.FindElement(By.Id("SearchInput")).GetAttribute("name") == "Search";
        }

        public bool IsOnPage_TechnologyList()
        {
            return _driver.FindElement(By.Id("TechTable")).Displayed;
        }

        public bool IsOnPage_Register()
        {
            return _driver.FindElement(By.Id("Input_ConfirmPassword")).Displayed;
        }

        public bool IsOnPage_Login()
        {
            return _driver.FindElement(By.Id("Input_RememberMe")).Displayed;
        }

        public bool IsOnPage_AddProject()
        {
            return _driver.FindElement(By.Id("Title")).GetAttribute("name") == "Title";
        }

        public bool IsOnEmptyProjectListAfterSearch()
        {
            return _driver.FindElement(By.Id("Please")).Text == "Please try again.";
        }

        public void ClickCookieConsent()
        {
            _driver.FindElement(By.XPath("//*[@id=\"cookieConsent\"]/div/div[2]/div/button")).Click();
        }

        public void LoginOwner()
        {
            _driver.FindElement(By.Id("Login")).Click();
            _driver.FindElement(By.Id("Input_Email")).SendKeys("TestUser@a.com");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("Qwedsa11!");
            _driver.FindElement(By.Id("Input_Password")).SendKeys(Keys.Enter);
        }

        public void LoginCoder()
        {
            _driver.FindElement(By.Id("Login")).Click();
            _driver.FindElement(By.Id("Input_Email")).SendKeys("Coder@a.com");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("Qwedsa11!1");
            _driver.FindElement(By.Id("Input_Password")).SendKeys(Keys.Enter);
        }

        public void Logout()
        {
            _driver.FindElement(By.XPath("//*[@id=\"logoutForm\"]/ul/li[2]/button")).Click();
        }

        public void PrepareDB()
        {
            using (SqlConnection SQLConnect =
                new SqlConnection(Configuration.ConnectionString))
            {
                SQLConnect.Open();

                SqlCommand ClearDB = new SqlCommand(
                    "Delete from ProjectMember " +
                    "Delete from UserTechnologyLevel " +
                    "Delete From ProjectTechnology " +
                    "Delete From Project " +
                    "DBCC CHECKIDENT('Project', RESEED, 0) "+
                    "Delete From AspNetUsers;", SQLConnect);

                SqlCommand Populate_Users = new SqlCommand(
                    "Declare @UId Nvarchar(450) Set @UId = '7e514a25-c4ab-4214-8414-f9ecc6dd9e9e'  "+
                    "Declare @UId2 Nvarchar(450) Set @UId2 = 'fd4d0e37-0731-4b55-9e7c-b522978a63cb'  "+
                    "Insert Into AspNetUsers (Id, UserName, NormalizedUserName, " +
                    "Email, NormalizedEmail, PasswordHash, SecurityStamp, ConcurrencyStamp, EmailConfirmed,  " +
                    "PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) Values " +
                    "(@UId, 'TestUser@a.com','TESTUSER@A.COM', 'TestUser@a.com', 'TESTUSER@A.COM', " +
                    "'AQAAAAEAACcQAAAAEMjfV0AsErgwEJqu3B/qoRZupd0gee/MR+kTVqHSiw+hyQRqVkwqckRqnX8KWszcAQ==', " +
                    "'22LCTUSUQEQ3TRILLNAI4UYKWW35DMU4', '9daa59f7-3e7d-43fa-a162-0b10d51fac57', 0, 0, 0, 1, 0)," +
                    
                    "(@UId2, 'coder@a.com', 'CODER@A.COM', 'coder@a.com', 'CODER@A.COM', " +
                    "'AQAAAAEAACcQAAAAEJlMmu9bbvEoXU0HgptFcer92br0MuNyDOrgEAU2Ida224ErGE80+/FE67lavr43Ng==', " +
                    "'2LY6TUSKFRDEW7TUDNQBWUZEGGKHMBNV', '162697f6-0ce8-494c-8025-bccb535120bd', 0, 0, 0, 1, 0 );", SQLConnect);

                SqlCommand Populate_Projects = new SqlCommand(
                    "Declare @UId Nvarchar(450) Set @UId = '7e514a25-c4ab-4214-8414-f9ecc6dd9e9e'  "+
                    "Insert Into Project(Title, Description, OwnerId, NewMembers, StateId) " +
                    "Values('FirstProject', 'Our first project will be SUPRISE Hello World', @UId, 0, 2)," +
                    "('SecondProject', 'Yes we will print Hello Kity', @UId, 0, 1)," +
                    "('Funny bunny', 'We want to create web aplication with many funny bunes', @UId, 1, 1)," +
                    "(N'Polish title like pamięć', N'Test for polish worlds like pamięć', @UId, 0, 1)," +
                    "('Test for adding Project with four Tech', 'Test for adding Project with four Technologies', @UId, 0, 1)," +
                    "('Project with Two Tech', 'Project with Two Tech (Test)', @UId, 0, 1) "
                    +
                    "Insert Into ProjectTechnology Values(5,2),(5, 4),(5, 6),(5, 7),(6, 6),(6, 7);" +
                    "INSERT INTO ProjectMember (ProjectId,MemberId,MessageDate,Message,AddMember) VALUES " +
                    "(1,'fd4d0e37-0731-4b55-9e7c-b522978a63cb','2019-02-02','Message',0)", SQLConnect);

                {
                    ClearDB.ExecuteNonQuery();
                    Populate_Users.ExecuteNonQuery();
                    Populate_Projects.ExecuteNonQuery();
                }
                SQLConnect.Close();
            }
        }
    }
}