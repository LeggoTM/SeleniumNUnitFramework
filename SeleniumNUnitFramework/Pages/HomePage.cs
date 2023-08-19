using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitFramework.Pages
{
    internal class HomePage
    {
        private readonly IWebDriver _driver;
        public HomePage(IWebDriver driver) => _driver = driver;

        // Navbar Links
        private By WebsiteLogo = By.XPath("//img[@alt=\"Website for automation practice\"]");
        private By HomeButton = By.XPath("//a[contains(text(), \"Home\")]");
        private By ProductsButton = By.XPath("//a[contains(text(), 'Products')]");
        private By CartButton = By.XPath("//a[contains(text(), 'Cart')]");
        private By SignupLoginButton = By.XPath("//a[contains(text(), 'Signup / Login')]");
        private By TestCasesButton = By.XPath("//a[contains(text(), 'Test Cases')]");
        private By APITestingButton = By.XPath("//a[contains(text(), 'API Testing')]");
        private By VideoTutorialsButton = By.XPath("//a[contains(text(), 'Video Tutorials')]");
        private By ContactUsButton = By.XPath("//a[contains(text(), 'Contact us')]");


        public SignUpPage NavigateToSignupLoginPage()
        {
            _driver.FindElement(SignupLoginButton).Click();
            return new SignUpPage(_driver);
        }
    }
}
