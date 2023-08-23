using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SeleniumNUnitFramework.Utilities;
using System.Configuration;

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
        public ExtentReports extentReport;
        public ExtentTest currentTest;
        protected ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        protected string baseURL = "https://www.automationexercise.com";

        [OneTimeSetUp]
        public void Setup()
        {
            string currentWorkingDirectory = Environment.CurrentDirectory;
            string currentProjectDirectory = Directory.GetParent(currentWorkingDirectory).Parent.Parent.FullName;
            string reportPath = currentProjectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extentReport = new ExtentReports();
            extentReport.AttachReporter(htmlReporter);
        }

        [SetUp]
        public void InitSetup()
        {
            currentTest = extentReport.CreateTest(TestContext.CurrentContext.Test.Name);
            InitBrowser();
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Navigate().GoToUrl(baseURL);
            driver.Value.Manage().Window.Maximize();
            //driver.Value.Manage().Window.Size = new Size(1366, 768);
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
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var testStackTrace = TestContext.CurrentContext.Result.StackTrace;
            var testRunMessage = TestContext.CurrentContext.Result.Message;

            DateTime currentTime = DateTime.Now;
            string screenShotName = "Screenshot " + currentTime.ToString("dd_MM_yyyy_HH_mm_ss") + ".png";

            if (testStatus.Equals(TestStatus.Skipped))
            {
                currentTest.Skip("Test Skipped");
            }
            else if (testStatus.Equals(TestStatus.Failed))
            {
                currentTest.Fail("Test Failed", CaptureScreenshot(driver: driver.Value, screenShotName));
                currentTest.Log(Status.Fail, $"Stacktrace: {testStackTrace} Message: {testRunMessage}");
            }
            else if (testStatus.Equals(TestStatus.Passed))
            {
                currentTest.Pass("Test Passed");
            }

            extentReport.Flush();
            driver.Value.Quit();
        }

        public MediaEntityModelProvider CaptureScreenshot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot screenShotObj = (ITakesScreenshot)driver;
            var screenCapturedStr = screenShotObj.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenCapturedStr, screenShotName).Build();
        }

        public static JsonReader GetJsonParser()
        {
            return new JsonReader();
        }
    }
}
