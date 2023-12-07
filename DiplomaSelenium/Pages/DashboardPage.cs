using Common;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace DiplomaSelenium.Pages;

public class DashboardPage : BasePage
{
    public List<string> GetDashBoardElementsTitles()
    {
        var dashboardElements = _driver.GetWait().Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//div[@class='orangehrm-dashboard-widget-name']//p")));

        var dashboardElementsTitles = dashboardElements.Select(x => x.Text).ToList();
        return dashboardElementsTitles;
    }
}