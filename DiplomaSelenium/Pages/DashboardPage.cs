using Common;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace DiplomaSelenium.Pages;

public class DashboardPage : BasePage
{
    public DashboardPage(IWebDriver driver) : base(driver)
    {
        Driver = driver;
    }

    public List<string> GetDashBoardElementsTitles()
    {
        var dashboardElements = Driver.GetWait().Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@class='orangehrm-dashboard-widget-name']//p")));

        var dashboardElementsTitles = dashboardElements.Select(x => x.Text).ToList();
        return dashboardElementsTitles;
    }
}
