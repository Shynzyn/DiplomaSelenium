using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Common;

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

    public static bool IsAlertPresent(this IWebDriver driver)
    {
        var alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
        return alert != null;
    }

    public static void ClearLocalStorage(this IWebDriver driver)
    {
        var js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("window.localStorage.clear();");
    }

    public static void ClearField(this IWebElement element)
    {
        element.SendKeys(Keys.Control + "a" + Keys.Delete);
    }

    public static void SelectOption(this IWebDriver driver, string spanText)
    {
        var option = driver.GetWait().Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[@role='option']/span[contains(., '{spanText}')]")));
        option.Click();
    }

    public static void ClickElement(this IWebDriver driver, By by)
    {
        var elementToClick = driver.GetWait().Until(ExpectedConditions.ElementIsVisible(by));
        elementToClick.Click();
    }

    public static void WaitForElementToBeGone(this IWebDriver driver, By by)
    {
        var element = driver.GetWait().Until(ExpectedConditions.ElementIsVisible(by));
        driver.GetWait().Until(ExpectedConditions.StalenessOf(element));
    }

    public static Actions GetActions(this IWebDriver driver)
    {
        return new Actions(driver);
    }
}