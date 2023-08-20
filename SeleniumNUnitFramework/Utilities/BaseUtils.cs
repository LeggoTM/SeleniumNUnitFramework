using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumNUnitFramework.Utilities
{
    internal class BaseUtils
    {
        public static void WaitUntilElementIsDisplayed(IWebDriver driver, By locator, int timeoutInSeconds = 10)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(driver => driver.FindElement(locator).Displayed);
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"Timeout waiting for element to be displayed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
