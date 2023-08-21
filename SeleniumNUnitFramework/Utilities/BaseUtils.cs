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
        public static IWebDriver SetupWebDriver()
        {
            string defaultBrowser = ConfigurationManager.AppSettings["DefaultBrowser"];
            string cliBrowser = TestContext.Parameters["BrowserType"];

            string browserTypeStr = string.IsNullOrEmpty(cliBrowser) ? defaultBrowser : cliBrowser;
            if (!Enum.TryParse(browserTypeStr, out BrowserType browserType)) throw new ArgumentException("Invalid browser type specified");

            switch (browserType)
            {
                case BrowserType.Chrome:
                    return new ChromeDriver();
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                case BrowserType.Edge:
                    return new EdgeDriver();
                default:
                    throw new ArgumentException("Invalid browser type");
            }
        }

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
