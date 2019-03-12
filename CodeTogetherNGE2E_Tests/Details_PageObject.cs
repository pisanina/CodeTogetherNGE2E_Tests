using OpenQA.Selenium;
using System;
using System.Collections.Generic;

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

        public int GetProjectState()
        {
            return Convert.ToInt32(_driver.FindElement(By.Id("State")).FindElement
                    (By.CssSelector("option[Selected]")).GetAttribute("value"));
        }

        public List<int> GetSelectedTechnologies()
        {
            List<int> ListOfTech = new List<int>();
            var Lista = _driver.FindElement(By.Id("TechList")).FindElements
                    (By.CssSelector("option[Selected]"));

            foreach (var item in Lista)
            {
                ListOfTech.Add(Convert.ToInt32(item.GetAttribute("value")));
            }
            return ListOfTech;
        }

        public string GetMembers()
        {
           return _driver.FindElement(By.Id("Members")).GetAttribute("value");
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

        public void EditTitle(string newTitle)
        {
            var title = _driver.FindElement(By.Id("Title"));
            title.Clear();
            title.SendKeys(newTitle);
        }

        public void EditDescription(string newDescription)
        {
            var description =  _driver.FindElement(By.Id("Description"));
            description.Clear();
            description.SendKeys(newDescription);
        }

        public void EditNewMembers(bool check)
        {
            var newMembers = _driver.FindElement(By.Id("NewMembers"));

            if (newMembers.Selected != check)
                _driver.FindElement(By.Id("NewMembers")).Click();
        }

        public void SelectTechnology(int techId)
        {
            var TechList = _driver.FindElement(By.Id("TechList"));
            TechList.FindElement(By.CssSelector("option[value=\"" + techId + "\"]")).Click();
        }

        public void SelectProjectState(int stateId)
        {
            var ProjectStateList = _driver.FindElement(By.Id("State"));
            ProjectStateList.FindElement(By.CssSelector("option[value=\"" + stateId + "\"]")).Click();
        }

        public void EditSave()
        {
            _driver.FindElement(By.Id("Save")).Click();
        }

        public void EditMessage(string message)
        {
            _driver.FindElement(By.Id("RequestMessage")).SendKeys(message);
            _driver.FindElement(By.Id("Send")).Click();
        }

        public void ClickShowRequestsButton()
        {
            _driver.FindElement(By.Id("ShowRequest")).Click();
        }
        
        public bool ErrorDisplayed(string error)
        {
            return _driver.PageSource.Contains(error);
        }

        public bool IsTitleEditable()
        {
            return _driver.FindElement(By.Id("Title")).GetAttribute("ReadOnly")=="false";
        }

        public bool IsCreationDateEditable()
        {
            return _driver.FindElement(By.Id("CreationDate")).GetAttribute("ReadOnly") == "false";
        }

        public bool IsOwnerNameEditable()
        {
            return _driver.FindElement(By.Id("Owner")).GetAttribute("ReadOnly") == "false";
        }

        public bool IsDescriptionEditable()
        {
            return _driver.FindElement(By.Id("Description")).GetAttribute("ReadOnly") == "false";
        }

        public bool IsNewMembersEditable()
        {
            return _driver.FindElement(By.Id("NewMembers")).GetAttribute("ReadOnly") == "false";
        }

        public bool IsProjectStateEditable()
        {
            return _driver.FindElement(By.Id("State")).GetAttribute("ReadOnly") == "false";
        }

        public bool IsTechnologyListEditable()
        {
            return _driver.FindElement(By.Id("TechList")).GetAttribute("ReadOnly") == "false";
        }

        public bool IsSaveButtonOnPage()
        {
            try
            {
                _driver.FindElement(By.Id("Save"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsShowRequestsButtonOnPage()
        {
           return  _driver.FindElements(By.Id("ShowRequest")).Count==1; 
        }

    }
}