namespace Dynamic365Automation
{
    using System;
    using System.Threading;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Static class that helps implementing some flexible wait methods
    /// </summary>
    public static class WaitFor
    {
        /// <summary>
        /// Bool delegate that we can add a boolean value to check against
        /// </summary>
        public delegate bool BoolDelegate();

        /// <summary>
        /// Method that will wait until the given booldelegate is true or until the max wait time is reached. If max wait time reached it returns false
        /// </summary>
        /// <param name="delegateToWaitFor">
        /// The delegate to wait for.
        /// </param>
        /// <param name="maxWaitTime">
        /// The max wait time.
        /// </param>
        /// <returns>
        /// If max wait time reached it returns false. If delegate is true before reaching that treshold it returns true
        /// </returns>
        public static bool ToBeTrue(BoolDelegate delegateToWaitFor, int maxWaitTime = 10)
        {
            DateTime startTime = DateTime.Now;

            do
            {
                try
                {
                    if (delegateToWaitFor.Invoke())
                    {
                        return true;
                    }
                }
                catch
                {
                    // catch any exception
                }

                Thread.Sleep(50);
            }
            while (startTime.AddSeconds(maxWaitTime) > DateTime.Now);

            return false;
        }

        /// <summary>
        /// Waits until the current page is fully loaded
        /// </summary>
        /// <param name="driver">
        /// The driver instance
        /// </param>
        public static void PageLoad(IWebDriver driver)
        {
            IJavaScriptExecutor javascriptDriver = driver as IJavaScriptExecutor;

            if (javascriptDriver != null)
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(wdriver => javascriptDriver.ExecuteScript("return document.readyState").Equals("complete"));
            }
        }
    }
}
