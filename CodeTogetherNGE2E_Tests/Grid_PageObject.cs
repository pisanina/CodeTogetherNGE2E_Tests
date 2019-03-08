using OpenQA.Selenium;

namespace CodeTogetherNGE2E_Tests
{
    internal class Grid_PageObject : Navigation_PageObject
    {
        public Grid_PageObject(IWebDriver driver) : base(driver)
        { }

        public void Search(string toSearch, bool submit = true)
        {
            var searchInput = _driver.FindElement(By.Id("SearchInput"));
            searchInput.Clear();
            searchInput.SendKeys(toSearch);
            if (submit)
                SearchSubmit();
        }

        public void SearchSubmit()
        {
            _driver.FindElement(By.Id("SearchInput")).SendKeys(Keys.Enter);
        }

        public void ClickTheFirstProject()
        {
            _driver.FindElement(By.XPath("/html/body/div/div/div[1]/a")).Click();
        }

        public bool IsTechnologiesDisplayed(int projectId, string technologies)
        {
            return _driver.FindElement(By.Id("project_" + projectId)).
                     FindElement(By.CssSelector("small")).Text == technologies;
        }

        public bool IsNewMembersDisplayed(int projectId)
        {
          return  _driver.FindElement(By.Id("project_" + projectId)).
                     FindElement(By.Id("newMembers-icon")).Displayed;
        }

        public void SelectTechnology(int techId)
        {
            var TechList = _driver.FindElement(By.Id("TechList"));
            TechList.FindElement(By.CssSelector("option[value=\"" + techId + "\"]")).Click();
        }

        public void SelectProjectState(int? stateId)
        {
            var ProjectStateList = _driver.FindElement(By.Id("State"));
            ProjectStateList.FindElement(By.CssSelector("option[value=\"" + stateId + "\"]")).Click();
        }

        public void SelectNewMembers(string newMembers)
        {
            var newMembersList = _driver.FindElement(By.Id("NewMembers"));
            newMembersList.FindElement(By.CssSelector("option[value=\"" + newMembers + "\"]")).Click();
        }

        public int GetProjectCount()
        {
            return _driver.FindElements(By.CssSelector(".list-group-item")).Count;
        }

        public bool IsProjectDisplayed(string name)
        {
            return _driver.PageSource.Contains(name, System.StringComparison.InvariantCultureIgnoreCase);
        }

        public string GetSearch()
        {
            return _driver.FindElement(By.Id("SearchInput")).GetAttribute("value");
        }

        public bool IsSearchInputRequired()
        {
            return _driver.FindElement(By.Id("SearchInput")).GetAttribute("required") == "true";
        }

        public void ClickButtonPleaseTryAgain()
        {
            _driver.FindElement(By.Id("Please")).Click();
        }
    }
}