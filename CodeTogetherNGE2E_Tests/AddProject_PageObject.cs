using OpenQA.Selenium;
using System.Collections.Generic;

namespace CodeTogetherNGE2E_Tests
{
    public class AddProject_PageObject : Navigation_PageObject
    {
        public AddProject_PageObject(IWebDriver driver) : base(driver)
        { }

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

        public void AddProject(string title, string description)
        {
            IEnumerable<int> techIdList = new List<int>();
            AddProject(title, description, techIdList);
        }

        public bool ErrorDisplayed(string error)
        {
            return _driver.PageSource.Contains(error);
        }
    }
}