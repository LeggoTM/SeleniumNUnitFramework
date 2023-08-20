using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumNUnitFramework.Tests
{
    [TestFixture]
    internal class BaseTest
    {
        protected IWebDriver driver;
        protected string baseURL = "https://www.automationexercise.com";

        [SetUp]
        public void InitSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl(baseURL);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void QuitAll()
        {
            driver.Quit();
        }
    }
}
