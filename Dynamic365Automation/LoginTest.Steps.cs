namespace Dynamic365Automation
{
    using System;

    using LightBDD.NUnit3;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;

    /// <summary>
    /// The login test implementations
    /// </summary>
    public partial class LoginTest : FeatureFixture
    {
        /// <summary>
        /// The current browser type which is set (injected) in the ctor
        /// </summary>
        private readonly BrowserType currentType;

        /// <summary>
        /// The current webdriver instance
        /// </summary>
        private IWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginTest"/> class.
        /// </summary>
        /// <param name="browser">
        /// The browser type which we would like to run our tests on.
        /// </param>
        public LoginTest(BrowserType browser)
        {
            this.currentType = browser;
        }

        /// <summary>
        /// The setup fixture. It runs before this test fixture. It sets the currently used browser. 
        /// </summary>
        [OneTimeSetUp]
        public void SetupFixture()
        {
            switch (this.currentType)
            {
                case BrowserType.Chrome:
                    this.driver = new ChromeDriver();
                break;
                case BrowserType.Firefox:
                    this.driver = new FirefoxDriver();
                break;
                case BrowserType.Edge:
                    this.driver = new EdgeDriver();
                break;
            }

            this.driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// The tear down method which closes the driver instance and closes the browser.
        /// </summary>
        [OneTimeTearDown]
        public void TearDown()
        {
            this.driver.Quit();
        }

        /// <summary>
        /// Navigate to main page and wait until it's loaded
        /// </summary>
        private void Given_I_am_on_the_main_login_page()
        {
            Uri mainSite = new Uri("https://re-gister.crm2.dynamics.com");
            this.driver.Navigate().GoToUrl(mainSite);
            WaitFor.PageLoad(this.driver);
        }

        /// <summary>
        /// Sending the login email to the login input field 
        /// </summary>
        /// <param name="email">
        /// The current email address
        /// </param>
        private void When_I_write_in_my_email_address(string email)
        {
            By emailFieldLocator = By.XPath("//input[@type='email']");
            WaitFor.ToBeTrue(() => TestHelper.ElementVisible(this.driver, emailFieldLocator));

            this.driver.FindElement(emailFieldLocator).SendKeys(email);
        }

        /// <summary>
        /// Clicking the next button step
        /// </summary>
        private void And_I_click_next_button()
        {
            TestHelper.SubmitForm(this.driver);
        }

        /// <summary>
        /// Writing the password to the password field
        /// </summary>
        /// <param name="password">
        /// The current password
        /// </param>
        private void And_I_write_in_my_password(string password)
        {
            By passwordFieldLocator = By.XPath("//input[@type='password']");
            WaitFor.ToBeTrue(() => TestHelper.ElementVisible(this.driver, passwordFieldLocator));

            this.driver.FindElement(passwordFieldLocator).SendKeys(password);
        }

        /// <summary>
        /// Clicking the Sign In button
        /// </summary>
        private void And_I_click_sign_in_button()
        {
            TestHelper.SubmitForm(this.driver);
        }

        /// <summary>
        /// Checking if the Remember login modal appears, if yes clicking on the No button, if doesn't appear just checking if we arrived on the main logged in page.
        /// </summary>
        private void Then_I_should_be_arrive_on_the_main_logged_in_page()
        {
            try
            {
                // Checking if Remember login modal is visible, if yes handle it
                Assert.IsTrue(WaitFor.ToBeTrue(() => TestHelper.ElementVisible(this.driver, By.CssSelector("#idBtn_Back")), 3));

                IWebElement dontRememberLoginButton = this.driver.FindElement(By.CssSelector("#idBtn_Back"));
                dontRememberLoginButton.Click();

                // Waiting for the page to load, and check if we arrived at the logged in page.
                WaitFor.PageLoad(this.driver);
                WaitFor.ToBeTrue(() => TestHelper.ElementVisible(this.driver, By.CssSelector("#lpathRuntimeOverlay")));
            }
            catch (AssertionException assertionException)
            {
                // Catching exception if Remember login screen is not appeared
                // Assert that we arrived at the main logged in page
                Console.WriteLine(assertionException);
                WaitFor.PageLoad(this.driver);
                WaitFor.ToBeTrue(() => TestHelper.ElementVisible(this.driver, By.CssSelector("#lpathRuntimeOverlay")));
            }
        }
    }
}