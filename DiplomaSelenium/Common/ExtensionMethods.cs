using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace DiplomaSelenium.Common;

public static class ExtensionMethods
{
    public static WebDriverWait GetWait(
        this IWebDriver driver,
        int timeOutSeconds = 5,
        int pollingIntervalMilliseconds = 250,
        Type[]? exceptionsToIgnore = null)
    {
        var timeOut = TimeSpan.FromSeconds(timeOutSeconds);
        var pollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilliseconds);
        var clock = new SystemClock();
        var wait = new WebDriverWait(clock, driver, timeOut, pollingInterval);

        var exceptionsToIgnoreByDefault = new[]
        {
            typeof(StaleElementReferenceException),
            typeof(NoSuchElementException),
            typeof(StaleElementReferenceException),
            typeof(ElementClickInterceptedException)
        };

        wait.IgnoreExceptionTypes(exceptionsToIgnore ?? exceptionsToIgnoreByDefault);

        return wait;
    }

    public static void NextWindow(this IWebDriver driver)
    {
        foreach (var windowHandle in driver.WindowHandles)
        {
            if (windowHandle != driver.CurrentWindowHandle)
            {
                driver.SwitchTo().Window(windowHandle);
                break;
            }
        }
    }

    public static void ClearLocalStorage(this IWebDriver driver)
    {
        driver.GetJsExecutor().ExecuteScript("window.localStorage.clear();");
    }

    public static Actions GetActions(this IWebDriver driver)
    {
        return new Actions(driver);
    }

    public static IJavaScriptExecutor GetJsExecutor(this IWebDriver driver)
    {
        var jsExecutor = (IJavaScriptExecutor)driver;
        return jsExecutor;
    }
}