using OpenQA.Selenium;
using SeleniumNUnitFramework.Utilities;

namespace SeleniumNUnitFramework.Pages
{
    internal class HomePage : BaseUtils
    {
        private readonly IWebDriver _driver;
        public HomePage(IWebDriver driver) => _driver = driver;

        // Navbar Links
        private By websiteLogo = By.XPath("//img[@alt=\"Website for automation practice\"]");
        private By homeButton = By.XPath("//a[contains(text(), \"Home\")]");
        private By productsButton = By.XPath("//a[contains(text(), 'Products')]");
        private By cartButton = By.XPath("//a[contains(text(), 'Cart')]");
        private By signupLoginButton = By.XPath("//a[contains(text(), 'Signup / Login')]");
        private By testCasesButton = By.XPath("//a[contains(text(), 'Test Cases')]");
        private By apiTestingButton = By.XPath("//a[contains(text(), 'API Testing')]");
        private By videoTutorialsButton = By.XPath("//a[contains(text(), 'Video Tutorials')]");
        private By contactUsButton = By.XPath("//a[contains(text(), 'Contact us')]");
        private By logoutButton = By.XPath("//a[contains(text(), 'Logout')]");
        private By deleteAccountButton = By.XPath("//a[contains(text(), 'Delete Account')]");
        private By loggedInAs = By.XPath("//a[contains(text(), 'Logged in as')]");

        private By deletedMessageText = By.XPath("//*[@data-qa=\"account-deleted\"]");

        public LoginPage NavigateToSignupLoginPage()
        {
            _driver.FindElement(signupLoginButton).Click();
            return new LoginPage(_driver);
        }

        public string loggedInUser() => _driver.FindElement(loggedInAs).Text;
        public LoginPage ClickLogout()
        {
            _driver.FindElement(logoutButton).Click();
            return new LoginPage(_driver);
        }

        public void WaitForPageToLoad() => WaitUntilElementIsDisplayed(driver: _driver, locator: websiteLogo, timeoutInSeconds: 10);
        public string GetDeletedMessageText() => _driver.FindElement(deletedMessageText).Text;

        public void ClickDeleteAccount()
        {
            _driver.FindElement(deleteAccountButton).Click();
        }
    }
}
