using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTogetherNGE2E_Tests
{
    class Users_PageObject : Navigation_PageObject
    {
        public Users_PageObject(IWebDriver driver) : base(driver)
        {  }

        public void ClickOnUser(string userName)
        {
            _driver.FindElement(By.Id(userName)).Click();
        }

        public bool CheckOwnerCount(string ownerCount, string userName)
        {
            return _driver.FindElement(By.Id("ownerof_"+ userName)).Text == ownerCount;
        }

        public bool CheckMemberCount(string memberCount, string userName)
        {
            return _driver.FindElement(By.Id("memberof_"+ userName)).Text == memberCount;
        }

        public bool CheckBeginnerCount(string beginnerCount, string userName)
        {
            return _driver.FindElement(By.Id("beginnerin_"+ userName)).Text == beginnerCount;
        }

        public bool CheckAdvancedCount(string advancedCount, string userName)
        {
            return _driver.FindElement(By.Id("advancedin_"+ userName)).Text == advancedCount;
        }

        public bool CheckExpertCount(string expertCount, string userName)
        {
            return _driver.FindElement(By.Id("expertin_"+ userName)).Text == expertCount;
        }
    }
}
