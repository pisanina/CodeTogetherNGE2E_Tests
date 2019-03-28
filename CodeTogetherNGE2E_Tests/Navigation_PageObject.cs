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

        public void GoToUsers()
        {
            _driver.FindElement(By.Id("UsersList")).Click();
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

        public bool IsOnPage_Users()
        {
            return _driver.FindElement(By.Id("UsersListTable")).Displayed;
        }

        public bool IsOnPage_Profile()
        {
            return _driver.FindElement(By.Id("ProfileHeader")).Displayed;
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
        
    }
}