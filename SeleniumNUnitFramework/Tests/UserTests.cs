using SeleniumNUnitFramework.Pages;

namespace SeleniumNUnitFramework.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    internal class UserTests : BaseTest
    {
        public static IEnumerable<TestCaseData> TestDataForValidLogin()
        {
            yield return new TestCaseData(GetJsonParser().ExtractJsonData(tokenName: "validLoginEmail"), GetJsonParser().ExtractJsonData(tokenName: "validLoginPassword"));
        }

        public static IEnumerable<TestCaseData> TestDataForInvalidLogin()
        {
            yield return new TestCaseData(GetJsonParser().ExtractJsonData(tokenName: "invalidLoginEmail"), GetJsonParser().ExtractJsonData(tokenName: "invalidLoginPassword"));

        }

        [TestCase(TestName = "Signup new user and delete", Ignore = "Long test")]
        public void CreateAccountAndDelete()
        {
            HomePage homePage = new HomePage(GetDriver());
            homePage.WaitForPageToLoad();
            Assert.That(GetDriver().Title, Is.EqualTo("Automation Exercise"));

            LoginPage loginPage = homePage.NavigateToSignupLoginPage();
            loginPage.WaitForPageToLoad();
            Assert.That(GetDriver().Title, Is.EqualTo("Automation Exercise - Signup / Login"));

            SignUpPage signUpPage = loginPage.SignupAs(name: "SeleniumBeginner", email: "seleniumbeginner77@mail.com");
            signUpPage.WaitForPageToLoad();
            Assert.That(GetDriver().Title, Is.EqualTo("Automation Exercise - Signup"));

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

        //[TestCase(TestName = "Login and Logout with valid creds", Category = "Login")]
        [Test, TestCaseSource(nameof(TestDataForValidLogin))]
        [Category("Login")]
        public void LoginAndLogoutValid(string email, string password)
        {
            HomePage homePage = new HomePage(GetDriver());
            homePage.WaitForPageToLoad();
            Assert.That(GetDriver().Title, Is.EqualTo("Automation Exercise"));

            LoginPage loginPage = homePage.NavigateToSignupLoginPage();
            loginPage.WaitForPageToLoad();
            HomePage loggedInPage = loginPage.LoginAs(email, password);
            loggedInPage.WaitForPageToLoad();
            Assert.That(loggedInPage.GetLoggedInUser(), Does.Contain("Valid User"));
            LoginPage loggedOutPage = loggedInPage.ClickLogout();
            loggedOutPage.WaitForPageToLoad();
            Assert.That(GetDriver().Title, Is.EqualTo("Automation Exercise - Signup / Login"));
        }

        //[TestCase(TestName = "Login and Logout with invalid creds", Category = "Login")]
        [Test, TestCaseSource(nameof(TestDataForInvalidLogin))]
        [Category("Login")]
        public void LoginInvalid(string email, string password)
        {
            HomePage homePage = new HomePage(GetDriver());
            homePage.WaitForPageToLoad();
            Assert.That(GetDriver().Title, Is.EqualTo("Automation Exercise"));

            LoginPage loginPage = homePage.NavigateToSignupLoginPage();
            loginPage.WaitForPageToLoad();
            loginPage.LoginAs(email, password);
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Your email or password is incorrect!"));
        }

        [TestCase(TestName = "Register existing user")]
        [NonParallelizable]
        public void RegisterExistingUser()
        {
            HomePage homePage = new HomePage(GetDriver());
            homePage.WaitForPageToLoad();
            Assert.That(GetDriver().Title, Is.EqualTo("Automation Exercise"));

            LoginPage loginPage = homePage.NavigateToSignupLoginPage();
            loginPage.WaitForPageToLoad();
            Assert.That(GetDriver().Title, Is.EqualTo("Automation Exercise - Signup / Login"));

            loginPage.SignupAs(name: "SeleniumBeginner", email: "validuser77@mail.com");
            Assert.That(loginPage.GetErrorMessage(), Is.EqualTo("Email Address already exist!"));
        }
    }
}
