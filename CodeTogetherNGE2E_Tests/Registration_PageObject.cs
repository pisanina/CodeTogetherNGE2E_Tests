using OpenQA.Selenium;

namespace CodeTogetherNGE2E_Tests
{
    internal class Registration_PageObject : Navigation_PageObject
    {
        public Registration_PageObject(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement _emailInput
        {
            get { return _driver.FindElement(By.Id("Input_Email")); }
        }

        private IWebElement _passwordInput
        {
            get { return _driver.FindElement(By.Id("Input_Password")); }
        }

        private IWebElement _confirmPasswordInput
        {
            get { return _driver.FindElement(By.Id("Input_ConfirmPassword")); }
        }

        private IWebElement _submitButton
        {
            get { return _driver.FindElement(By.XPath("/html/body/div/div/div/form/button")); }
        }

        public void Insert_Email(string email)
        {
            _emailInput.SendKeys(email);
        }

        public void Insert_Password(string password)
        {
            _passwordInput.SendKeys(password);
        }

        public void Insert_ConfirmPassword(string confirmPassword)
        {
            _confirmPasswordInput.SendKeys(confirmPassword);
        }

        public void Click_Submit()
        {
            _submitButton.Click();
        }
    }
}