using OpenQA.Selenium;

namespace CodeTogetherNGE2E_Tests
{
    internal class Details_PageObject : Navigation_PageObject
    {
        public Details_PageObject(IWebDriver driver) : base(driver)
        { }

        public string GetTitle()
        {
            return _driver.FindElement(By.Id("Title")).GetAttribute("value");
        }

        public string GetDescription()
        {
            return _driver.FindElement(By.Id("Description")).GetAttribute("value");
        }

        public string GetOwner()
        {
            return _driver.FindElement(By.Id("Owner")).GetAttribute("value");
        }

        public string GetCreationDate()
        {
            return _driver.FindElement(By.Id("CreationDate")).GetAttribute("value");
        }

        public bool GetNewMembers()
        {
            return _driver.FindElement(By.Id("NewMembers")).GetAttribute("checked") == "true";
        }

        public bool IsOnDetailsView()
        {
            return _driver.FindElements(By.Id("SearchInput")).Count == 0
                && _driver.FindElement(By.Id("CreationDate")) != null;
        }

        public bool IsTechnologySelected(int id)
        {
            return _driver.FindElement(By.CssSelector("option[value='" + id + "']")).Selected;
        }
    }
}