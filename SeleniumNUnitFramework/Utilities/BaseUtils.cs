using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace SeleniumNUnitFramework.Utilities
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge
    }
    
    internal class BaseUtils
    {
        //public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        protected static IWebDriver driver;

        public static void WaitUntilElementIsDisplayed(IWebDriver driver, By locator, int timeoutInSeconds = 10)
        {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(driver => driver.FindElement(locator).Displayed);
        }
    }
}
