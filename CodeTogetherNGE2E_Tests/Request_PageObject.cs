using OpenQA.Selenium;

namespace CodeTogetherNGE2E_Tests
{
    internal class Requests_PageObject : Navigation_PageObject
    {
        public Requests_PageObject(IWebDriver driver) : base(driver)
        { }

        public void AcceptButtonClick()
        {
            _driver.FindElement(By.Id("Accept_coder@a.com")).Click();
        }

        public void DeclineButtonClick()
        {
            _driver.FindElement(By.Id("Decline_coder@a.com")).Click();
        }

        public bool CheckMessage(int Id, string message)
        {
            return _driver.FindElement(By.Id("project_" + Id)).
                    FindElement(By.Id("Message")).Text == message;
        }
    }
}