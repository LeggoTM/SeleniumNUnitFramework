﻿using OpenQA.Selenium;
using SeleniumNUnitFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitFramework.Pages
{
    internal class LoginPage : BaseUtils
    {
        private readonly IWebDriver _driver;
        public LoginPage(IWebDriver driver) => _driver = driver;

        // Login
        private By loginEmailField = By.XPath("//input[@data-qa=\"login-email\"]");
        private By loginPasswordField = By.XPath("//input[@data-qa=\"login-password\"]");
        private By loginButton = By.XPath("//*[@data-qa=\"login-button\"]");

        // Signup
        private By signupNameField = By.XPath("//input[@data-qa=\"signup-name\"]");
        private By signupEmailField = By.XPath("//input[@data-qa=\"signup-email\"]");
        private By signupButton = By.XPath("//*[@data-qa=\"signup-button\"]");

        private By errorMessage = By.XPath("//p[@style=\"color: red;\"]");

        public void TypeLoginEmail(string loginEmail) => _driver.FindElement(loginEmailField).SendKeys(loginEmail);
        public void TypeLoginPassword(string loginPassword) => _driver.FindElement(loginPasswordField).SendKeys(loginPassword);
        public void ClickLoginButton() => _driver.FindElement(loginButton).Click();
        public void TypeSignupName(string signupName) => _driver.FindElement(signupNameField).SendKeys(signupName);
        public void TypeSignupEmail(string signupEmail) => _driver.FindElement(signupEmailField).SendKeys(signupEmail);
        public void ClickSignupButton() => _driver.FindElement(signupButton).Click();
        public string GetErrorMessage() => _driver.FindElement(errorMessage).Text;

        public SignUpPage SignupAs(string name, string email)
        {
            TypeSignupName(name);
            TypeSignupEmail(email);
            ClickSignupButton();
            return new SignUpPage(_driver);
        }

        public HomePage LoginAs(string email,  string password)
        {
            TypeLoginEmail(email);
            TypeLoginPassword(password);
            ClickLoginButton();
            return new HomePage(_driver);
        }

        public void WaitForPageToLoad() => WaitUntilElementIsDisplayed(driver: _driver, locator: loginButton, timeoutInSeconds: 10);
    }
}
