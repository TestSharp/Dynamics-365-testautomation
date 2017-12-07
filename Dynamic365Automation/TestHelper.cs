namespace Dynamic365Automation
{
    using System;

    using OpenQA.Selenium;

    /// <summary>
    /// Test helper class. Contains members that help with the basic testing processes
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Safely checks (without throwing exception) if an element is visible or not
        /// </summary>
        /// <param name="driver">
        /// The driver instance to use
        /// </param>
        /// <param name="locator">
        /// The locator of the element which we will like to test
        /// </param>
        /// <returns>
        /// <see cref="bool"/>.
        /// </returns>
        public static bool ElementVisible(IWebDriver driver, By locator)
        {
            try
            {
                IWebElement element = driver.FindElement(locator);

                return element.Displayed && element.Enabled;
            }
            catch (NoSuchElementException nsee)
            {
                Console.WriteLine(nsee);
                return false;
            }
        }

        /// <summary>
        /// Checks if a given element is visible and enabled or not
        /// </summary>
        /// <param name="element">
        /// The element which we would like to check against
        /// </param>
        /// <returns>
        /// <see cref="bool"/>.
        /// </returns>
        public static bool ElementVisible(IWebElement element) => element.Displayed && element.Enabled;

        /// <summary>
        /// Submitting the current form
        /// </summary>
        /// <param name="driver">
        /// The driver instance
        /// </param>
        public static void SubmitForm(IWebDriver driver)
        {
            By submitButtonLocator = By.XPath("//input[@type='submit']");
            WaitFor.ToBeTrue(() => ElementVisible(driver, submitButtonLocator));

            IWebElement submitButton = driver.FindElement(submitButtonLocator);

            submitButton.Submit();
        }
    }
}
