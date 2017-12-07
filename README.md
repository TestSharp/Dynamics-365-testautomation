# Dynamics-365-testautomation
This is a sample testautomation project for Dynamics 365.

It uses Selenium, NUnit3 and LightBDD

In order to run you need either Visual Studio or NUnit3 console runner.
To see how to run without Visual Studio, check this article: http://testsharp.net/en/2017/11/21/how-to-run-test-automation-without-visual-studio/

Currently the tests run in sequential order. If you'd like to run parallel, you need to comment out the ParallelScope and LevelOfParalellism assembly attributes in the AssemblyInfo.cs (located in the Properties folder)
You need to be sure that the tests are able to run parallel (trying to login with same user doesn't create DB issues, and matters like this).

# The main parts of the solution:

- BrowserType.cs: An enum which contains the different types of browsers we would like to use during testing
- TestHelper.cs: A static class that contains helper methods for the tests
- WaitFor.cs: A static class that can help you with different flexible wait methods
- LoginTest.cs: The file that contains the LightBDD steps
- LoginTest.Steps.cs: The file that contains the implementation for the LightBDD steps