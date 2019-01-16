using OpenQA.Selenium;

namespace CodeTogetherNGE2E_Tests
{
    public class AddProject_TestsPageObject
    {
        private IWebDriver driver;

        public AddProject_TestsPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void AddProject(string Title, string Description)
        {
            driver.FindElement(By.Id("AddProject")).Click();
            driver.FindElement(By.Id("Title")).SendKeys(Title);
            driver.FindElement(By.Id("Description")).SendKeys(Description);
            driver.FindElement(By.Id("Create")).Click();
        }
    }
}