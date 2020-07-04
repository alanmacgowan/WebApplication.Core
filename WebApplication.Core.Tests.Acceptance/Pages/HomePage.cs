using OpenQA.Selenium;
using System.Configuration;
using WebApplication.Core.Tests.Acceptance.Base;

namespace WebApplication.Core.Tests.Acceptance.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public override string Url
        {
            get
            {
                return BaseUrl;
            }
        }
    
        public IWebElement employees_menu => Driver.FindElement(By.Id("employees_menu"));
        public IWebElement yearSpan => Driver.FindElement(By.Id("yearSpan"));
        
        public bool ValidatePage()
        {
            return DriverWait.Until(drv => employees_menu.Displayed);
        }

        public void ClickEmployeesMenu()
        {
            employees_menu.Click();
        }
    }
}
