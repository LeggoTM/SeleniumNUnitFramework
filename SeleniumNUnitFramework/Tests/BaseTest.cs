using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SeleniumNUnitFramework.Utilities;
using System.Configuration;
using System.Drawing;

namespace SeleniumNUnitFramework.Tests
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge
    }

    [TestFixture]
    internal class BaseTest
    {
        ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        //protected IWebDriver driver;
        protected string baseURL = "https://www.automationexercise.com";

        [SetUp]
        public void InitSetup()
        {
            InitBrowser();
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Navigate().GoToUrl(baseURL);
            //driver.Manage().Window.Maximize();
            driver.Value.Manage().Window.Size = new Size(1366, 768);
        }

        public IWebDriver GetDriver() { return driver.Value; }

        public void InitBrowser()
        {
            string defaultBrowser = ConfigurationManager.AppSettings["DefaultBrowser"];
            string cliBrowser = TestContext.Parameters["BrowserType"];

            string browserTypeStr = string.IsNullOrEmpty(cliBrowser) ? defaultBrowser : cliBrowser;
            if (!Enum.TryParse(browserTypeStr, out BrowserType browserType)) throw new ArgumentException("Invalid browser type specified");

            switch (browserType)
            {
                case BrowserType.Chrome:
                    driver.Value = new ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    driver.Value = new FirefoxDriver();
                    break;
                case BrowserType.Edge:
                    driver.Value = new EdgeDriver();
                    break;
                default:
                    throw new ArgumentException("Invalid browser type");
            }
        }

        [TearDown]
        public void QuitAll()
        {
            driver.Value.Quit();
        }

        public static JsonReader GetJsonParser()
        {
            return new JsonReader();
        }
    }
}
