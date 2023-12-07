using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Common;

public class BrowserFactory
{
    [ThreadStatic]
    private static IWebDriver? _driver;

    public static IWebDriver WebDriver => _driver!;

    public static IWebDriver GetDriver(BrowserType browserType)
    {
        return _driver ??= CreateDriverInstance(browserType);
    }

    public static IWebDriver GetDriver(BrowserType browserType, ChromeOptions options)
    {
        return _driver ??= CreateDriverInstance(browserType, options);
    }

    private static IWebDriver CreateDriverInstance(BrowserType browserType)
    {
        switch (browserType)
        {
            case BrowserType.Chrome:
                _driver = new ChromeDriver();
                break;
            case BrowserType.Firefox:
                _driver = new FirefoxDriver();
                break;
            case BrowserType.Edge:
                _driver = new EdgeDriver();
                break;
            default:
                throw new NotSupportedException($"Browser type '{browserType}' is not supported.");
        }

        return _driver;
    }

    private static IWebDriver CreateDriverInstance(BrowserType browserType, ChromeOptions options)
    {
        IWebDriver driver = null;
        driver = browserType == BrowserType.Chrome ? new ChromeDriver(options) : throw new NotSupportedException("Only ChromeOptions are supported");
        return driver;
    }

    public static void CloseDriver()
    {
        if (_driver != null)
        {
            _driver.Quit();
            _driver = null;
        }
    }
}

public enum BrowserType
{
    Chrome,
    Firefox,
    Edge,
}