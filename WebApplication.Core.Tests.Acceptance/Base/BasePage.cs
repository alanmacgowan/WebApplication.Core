using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Reflection;

namespace WebApplication.Core.Tests.Acceptance.Base
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait DriverWait;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            // wait 30 seconds.
            DriverWait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
        }

        public virtual string Url
        {
            get
            {
                return string.Empty;
            }
        }

        public string BaseUrl
        {
            get
            {
                return (ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location)).AppSettings.Settings["BASE_URL"].Value;
            }
        }

        public virtual void Open(string part = "")
        {
            if (string.IsNullOrEmpty(Url))
            {
                throw new ArgumentException("The main URL cannot be null or empty.");
            }

            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(string.Concat(Url, part));
        }
    }

}
