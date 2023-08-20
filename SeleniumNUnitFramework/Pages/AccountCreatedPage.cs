using OpenQA.Selenium;
using SeleniumNUnitFramework.Utilities;

namespace SeleniumNUnitFramework.Pages
{
    internal class AccountCreatedPage : BaseUtils
    {
        private readonly IWebDriver _driver;
        public AccountCreatedPage(IWebDriver driver) => _driver = driver;

        private By createdMessageText = By.XPath("//*[@data-qa=\"account-created\"]");
        private By continueButton = By.XPath("//*[@data-qa=\"continue-button\"]");

        public string GetCreatedMessageText() => _driver.FindElement(createdMessageText).Text;
        public HomePage ClickContinueButton()
        {
            _driver.FindElement(continueButton).Click();
            return new HomePage(_driver);
        }

        public void WaitForPageToLoad() => WaitUntilElementIsDisplayed(driver: _driver, locator: continueButton, timeoutInSeconds: 10);

    }
}
