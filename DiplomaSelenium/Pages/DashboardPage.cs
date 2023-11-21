using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class DashboardPage : BasePage
{
    public DashboardPage(IWebDriver driver) : base(driver)
    {
        Driver = driver;
    }
}
