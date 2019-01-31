using OpenQA.Selenium;

namespace CodeTogetherNGE2E_Tests
{
    public class AddProject_TestsPageObject
    {
        private IWebDriver _driver;

        public AddProject_TestsPageObject(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void AddProject(string Title, string Description)
        {
            _driver.FindElement(By.Id("AddProject")).Click();
            _driver.FindElement(By.Id("Title")).SendKeys(Title);
            _driver.FindElement(By.Id("Description")).SendKeys(Description);
            _driver.FindElement(By.Id("Create")).Click();
        }
    }
}