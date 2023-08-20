using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitFramework.Pages
{
    internal class AccountCreatedPage
    {
        private readonly IWebDriver _driver;
        public AccountCreatedPage(IWebDriver driver) => _driver = driver;

        private By messageText = By.XPath("//*[@data-qa=\"account-created\"]");
        private By continueButton = By.XPath("//*[@data-qa=\"continue-button\"]");

        public string GetMessageText() => _driver.FindElement(messageText).Text;
        public HomePage ClickContinueButton()
        {
            _driver.FindElement(continueButton).Click();
            return new HomePage(_driver);
        }
    }
}
