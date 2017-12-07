namespace Dynamic365Automation
{
    using LightBDD.Framework;
    using LightBDD.Framework.Scenarios.Extended;
    using LightBDD.NUnit3;

    using NUnit.Framework;

    /// <summary>
    /// The login test class
    /// </summary>
    [Label("Login")]
    [FeatureDescription(@"In order to see everything is working fine with the login functionality 
                          As a user
                          I want to be able to log in")]
    [TestFixtureSource(nameof(browsers))]
    public partial class LoginTest
    {
        /// <summary>
        /// This array contains the iteration base. Currently it contains the different browsers we would like to run our test against
        /// </summary>
        private static BrowserType[] browsers = { BrowserType.Chrome, BrowserType.Firefox, BrowserType.Edge };

        /// <summary>
        /// The test that checks a simple login process with the given user.
        /// </summary>
        [Label("LoginWithDemoUser")]
        [Scenario]
        public void Login_with_demo_user()
        {
            Runner.RunScenario(
                _ => Given_I_am_on_the_main_login_page(),
                _ => When_I_write_in_my_email_address("peter.halassy@re-gister.com"),
                _ => And_I_click_next_button(),
                _ => And_I_write_in_my_password("P@ssword"),
                _ => And_I_click_sign_in_button(),
                _ => Then_I_should_be_arrive_on_the_main_logged_in_page());
        }
    }
}