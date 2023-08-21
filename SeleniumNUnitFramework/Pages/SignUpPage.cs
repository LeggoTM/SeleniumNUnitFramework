using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumNUnitFramework.Utilities;

namespace SeleniumNUnitFramework.Pages
{
    public enum TitleSelect
    {
        Mr,
        Mrs
    }

    public enum BirthMonths
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    public enum Country
    {
        India
    }

    internal class SignUpPage : BaseUtils
    {
        private readonly IWebDriver _driver;
        public SignUpPage(IWebDriver driver) => _driver = driver;

        // Account Information
        private By radioMr = By.XPath("//input[@value=\"Mr\"]");
        private By radioMrs = By.XPath("//input[@value=\"Mrs\"]");
        private By nameField = By.XPath("//input[@data-qa=\"name\"]");
        private By emailField = By.XPath("//input[@data-qa=\"email\"]");
        private By passwordField = By.XPath("//input[@data-qa=\"password\"]");
        private By birthDaysSelect = By.XPath("//select[@data-qa=\"days\"]");
        private By birthMonthSelect = By.XPath("//select[@data-qa=\"months\"]");
        private By birthYearSelect = By.XPath("//select[@data-qa=\"years\"]");
        private By signupNewsletterCheckbox = By.XPath("//*[@type=\"checkbox\" and @id=\"newsletter\"]");
        private By splOffersCheckbox = By.XPath("//*[@type=\"checkbox\" and @id=\"optin\"]");

        // Address Information
        private By firstNameField = By.XPath("//*[@data-qa=\"first_name\"]");
        private By lastNameField = By.XPath("//*[@data-qa=\"last_name\"]");
        private By companyField = By.XPath("//*[@data-qa=\"company\"]");
        private By addressFirstField = By.XPath("//*[@data-qa=\"address\"]");
        private By addressSecondField = By.XPath("//*[@data-qa=\"address2\"]");
        private By countrySelect = By.XPath("//select[@data-qa=\"country\"]");
        private By stateField = By.XPath("//*[@data-qa=\"state\"]");
        private By cityField = By.XPath("//*[@data-qa=\"city\"]");
        private By zipcodeField = By.XPath("//*[@data-qa=\"zipcode\"]");
        private By phoneField = By.XPath("//*[@data-qa=\"mobile_number\"]");

        private By createAccountButton = By.XPath("//*[@data-qa=\"create-account\"]");


        public void SelectTitle(TitleSelect titleSelect)
        {
            switch (titleSelect)
            {
                case TitleSelect.Mr:
                    _driver.FindElement(radioMr).Click();
                    break;
                case TitleSelect.Mrs:
                    _driver.FindElement(radioMrs).Click();
                    break;
                default:
                    throw new ArgumentException("Invalid title");
            }
        }

        public void SelectBirthDay(int birthDay)
        {
            SelectElement birthDayElement = new SelectElement(_driver.FindElement(birthDaysSelect));
            birthDayElement.SelectByText(birthDay.ToString());
        }

        public void SelectBirthMonth(BirthMonths birthMonth)
        {
            SelectElement birthMonthElement = new SelectElement(_driver.FindElement(birthMonthSelect));
            birthMonthElement.SelectByText(birthMonth.ToString());
        }

        public void SelectBithYear(int birthYear)
        {
            SelectElement birthYearElement = new SelectElement(_driver.FindElement(birthYearSelect));
            birthYearElement.SelectByText(birthYear.ToString());
        }

        public void SelectCountry(Country country)
        {
            SelectElement countrySelectElement = new SelectElement(_driver.FindElement(countrySelect));
            countrySelectElement.SelectByText(country.ToString());
        }

        public void TypeSignupPassword(string pswd) => _driver.FindElement(passwordField).SendKeys(pswd);
        public void SignupNewsletter() => _driver.FindElement(signupNewsletterCheckbox).Click();
        public void OptSplOffer() => _driver.FindElement(splOffersCheckbox).Click();
        public void TypeFirstName(string firstName) => _driver.FindElement(firstNameField).SendKeys(firstName);
        public void TypeLastName(string lastName) => _driver.FindElement(lastNameField).SendKeys(lastName);
        public void TypeCompany(string company) => _driver.FindElement(companyField).SendKeys(company);
        public void TypeFirstAddress(string address1) => _driver.FindElement(addressFirstField).SendKeys(address1);
        public void TypeSecondAddress(string address2) => _driver.FindElement(addressSecondField).SendKeys(address2);
        public void TypeState(string state) => _driver.FindElement(stateField).SendKeys(state);
        public void TypeCity(string city) => _driver.FindElement(cityField).SendKeys(city);
        public void TypeZipcode(int zipcode) => _driver.FindElement(zipcodeField).SendKeys(zipcode.ToString());
        public void TypePhoneNumber(int phoneNumber) => _driver.FindElement(phoneField).SendKeys(phoneNumber.ToString());
        public void ClickCreateAccout() => _driver.FindElement(createAccountButton).Click();

        public AccountCreatedPage CreateAccountAs(
            TitleSelect title,
            string password,
            int birthDay,
            BirthMonths birthMonth,
            int birthYear,
            bool signupNewsletter,
            bool optSplOffer,
            string firstName,
            string lastName,
            string company,
            string address1,
            Country country,
            string state,
            string city,
            int zipcode,
            int phoneNumber,
            string? address2 = null
            )
        {
            SelectTitle(title);
            TypeSignupPassword(password);
            SelectBirthDay(birthDay);
            SelectBirthMonth(birthMonth);
            SelectBithYear(birthYear);
            if (signupNewsletter) SignupNewsletter();
            if (optSplOffer) OptSplOffer();
            TypeFirstName(firstName);
            TypeLastName(lastName);
            TypeCompany(company);
            TypeFirstAddress(address1);
            if (address2 != null) TypeSecondAddress(address2);
            SelectCountry(country);
            TypeState(state);
            TypeCity(city);
            TypeZipcode(zipcode);
            TypePhoneNumber(phoneNumber);
            ClickCreateAccout();
            return new AccountCreatedPage(_driver);
        }

        public void WaitForPageToLoad() => WaitUntilElementIsDisplayed(driver: _driver, locator: radioMr, timeoutInSeconds: 10);

    }
}
