using OpenQA.Selenium;

namespace CodeTogetherNGE2E_Tests
{
    internal class Profile_PageObject : Navigation_PageObject
    {
        public Profile_PageObject(IWebDriver driver) : base(driver)
        { }

        public void ClickOnUser(string userName)
        {
            _driver.FindElement(By.Id("User_" + userName)).Click();
        }

        public void ClickOnAdd()
        {
            _driver.FindElement(By.Id("Add")).Click();
        }

        public void ClickOnAddRole()
        {
            _driver.FindElement(By.Id("AddITRole")).Click();
        }

        public void ClickOnDelete(string techName)
        {
            _driver.FindElement(By.Id("Delete_" + techName)).Click();
        }

        public void ClickOnDeleteITRole(string role)
        {
            _driver.FindElement(By.Id("Delete_" + role)).Click();
        }

        public void SelectTechnology(int techId)
        {
            var TechList = _driver.FindElement(By.Id("TechList"));
            TechList.FindElement(By.CssSelector("option[value=\"" + techId + "\"]")).Click();
        }

        public void SelectITRole(int roleId)
        {
            var TechList = _driver.FindElement(By.Id("ITRoleList"));
            TechList.FindElement(By.CssSelector("option[value=\"" + roleId + "\"]")).Click();
        }

        public void SelectSkillLevel(int skillId)
        {
            var TechList = _driver.FindElement(By.Id("Level"));
            TechList.FindElement(By.CssSelector("option[value=\"" + skillId + "\"]")).Click();
        }

        public bool CheckNameOfSkill(string techName)
        {
            return _driver.FindElement(By.Id("userskill_" + techName)).Displayed;
        }

        

        public bool CheckLevelOfSkill(string techName, string techLevel)
        {
            return _driver.FindElement(By.Id("skilllevel_" + techName)).Text == techLevel;
        }

        public bool CheckITRoleAdded(string roleName)
        {
            return _driver.FindElement(By.Id("usersrole_" + roleName)).Displayed;
        }

        public void ClickOnProjectTitle(string projectTitle)
        {
            _driver.FindElement(By.Id(projectTitle)).Click();
        }

        public bool CheckUserNameInHeader(string userName)
        {
            return _driver.FindElement(By.Id("ProfileHeader")).Text == ("Profile of " + userName);
        }

        public bool IsNoSkillsMessageDisplayed()
        {
            return _driver.FindElement(By.Id("noskill")).Displayed;
        }

        public bool IsNoITRoleMessageDisplayed()
        {
            return _driver.FindElement(By.Id("noroles")).Displayed;
        }

        public bool IsNoProjectMessageDisplayed()
        {
            return _driver.FindElement(By.Id("noproject")).Displayed;
        }

        public bool IsAddButtonDisplayed()
        {
            return _driver.FindElements(By.Id("Add")).Count == 1;
        }

        public bool IsAddITRoleButtonDisplayed()
        {
            return _driver.FindElements(By.Id("AddITRole")).Count == 1;
        }

        public bool IsITRoleListAddDisplayed()
        {
            return _driver.FindElements(By.Id("ITRoleList")).Count == 1;
        }

        public bool IsTechListAddDisplayed()
        {
            return _driver.FindElements(By.Id("TechList")).Count == 1;
        }

        public bool IsSkillLevelAddDisplayed()
        {
            return _driver.FindElements(By.Id("Level")).Count == 1;
        }

        public bool IsProjectDisplayed(string title)
        {
            return _driver.FindElements(By.Id(title)).Count == 1;
        }

        public int HowManySkills()
        {
           return  _driver.FindElement(By.Id("TechTable")).FindElements(By.CssSelector("tr")).Count-1;
        }

        public int HowManyITRoles()
        {
            return _driver.FindElement(By.Id("RolesTable")).FindElements(By.CssSelector("tr")).Count - 1;
        }

        public int HowManyProjects()
        {
            return _driver.FindElement(By.Id("ProjectsTable")).FindElements(By.CssSelector("tr")).Count-1;
        }
    }
}