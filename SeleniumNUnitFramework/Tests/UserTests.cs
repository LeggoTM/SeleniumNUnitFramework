using SeleniumNUnitFramework.Pages;

namespace SeleniumNUnitFramework.Tests
{
    [TestFixture]
    internal class UserTests : BaseTest
    {
        [Test]
        public void CreateAccountAndDelete()
        {
            HomePage homePage = new HomePage(driver);
            homePage.WaitForPageToLoad();
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise"));

            LoginPage loginPage = homePage.NavigateToSignupLoginPage();
            loginPage.WaitForPageToLoad();
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise - Signup / Login"));

            SignUpPage signUpPage = loginPage.SignupAs(name: "SeleniumBeginner", email: "seleniumbeginner77@mail.com");
            signUpPage.WaitForPageToLoad();
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise - Signup"));

            AccountCreatedPage accountCreatedPage = signUpPage.CreateAccountAs(
                title: TitleSelect.Mr,
                password: "selenium123",
                birthDay: 17,
                birthMonth: BirthMonths.August,
                birthYear: 2000,
                signupNewsletter: true,
                optSplOffer: true,
                firstName: "Selenium",
                lastName: "Beginner",
                company: "Mindfire",
                address1: "India",
                country: Country.India,
                state: "Odisha",
                city: "BBSR",
                zipcode: 123456,
                phoneNumber: 1234567890
                );
            accountCreatedPage.WaitForPageToLoad();
            Assert.That(accountCreatedPage.GetCreatedMessageText(), Is.EqualTo("ACCOUNT CREATED!"));

            HomePage loggedInPage = accountCreatedPage.ClickContinueButton();
            loggedInPage.WaitForPageToLoad();
            Assert.That(loggedInPage.GetLoggedInUser(), Does.Contain("SeleniumBeginner"));
            loggedInPage.ClickDeleteAccount();
            Assert.That(loggedInPage.GetDeletedMessageText(), Is.EqualTo("ACCOUNT DELETED!"));
        }

        [Test]
        [Category("Login")]
        public void LoginAndLogoutValid()
        {
            HomePage homePage = new HomePage(driver);
            homePage.WaitForPageToLoad();
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise"));

            LoginPage loginPage = homePage.NavigateToSignupLoginPage();
            loginPage.WaitForPageToLoad();
            HomePage loggedInPage = loginPage.LoginAs("validuser77@mail.com", "validuser123");
            loggedInPage.WaitForPageToLoad();
            Assert.That(loggedInPage.GetLoggedInUser(), Does.Contain("Valid User"));
            LoginPage loggedOutPage = loggedInPage.ClickLogout();
            loggedOutPage.WaitForPageToLoad();
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise - Signup / Login"));
        }

        [Test]
        [Category("Login")]
        public void LoginInvalid()
        {
            HomePage homePage = new HomePage(driver);
            homePage.WaitForPageToLoad();
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise"));

            LoginPage loginPage = homePage.NavigateToSignupLoginPage();
            loginPage.WaitForPageToLoad();
            loginPage.LoginAs("validuser77@mail.com", "valid");
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Your email or password is incorrect!"));
        }

        [Test]
        public void RegisterExistingUser()
        {
            HomePage homePage = new HomePage(driver);
            homePage.WaitForPageToLoad();
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise"));

            LoginPage loginPage = homePage.NavigateToSignupLoginPage();
            loginPage.WaitForPageToLoad();
            Assert.That(driver.Title, Is.EqualTo("Automation Exercise - Signup / Login"));

            loginPage.SignupAs(name: "SeleniumBeginner", email: "validuser77@mail.com");
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Email Address already exist!"));
        }
    }
}
