using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumNUnitFramework.Utilities
{

    internal class BaseUtils
    {
        protected IWebDriver driver;

        public static void WaitUntilElementIsDisplayed(IWebDriver driver, By locator, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(driver => driver.FindElement(locator).Displayed);
        }
    }
}
