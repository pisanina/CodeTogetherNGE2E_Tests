using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTogetherNGE2E_Tests
{
    public abstract class TestsBase
    {
        protected IRepository repo;
        protected IWebDriver driver;

        protected void Setup()
        {
            repo = new Repository();
            repo.PrepareDbBeforeTest();

            driver = new ChromeDriver(Configuration.WebDriverLocation);
            driver.Url = Configuration.WebApiUrl;
        }

    }
}
