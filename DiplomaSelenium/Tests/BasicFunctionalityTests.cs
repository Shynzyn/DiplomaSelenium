using Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;

namespace DiplomaSelenium.Tests;

public class BasicFunctionalityTests : BaseTest
{
    [SetUp]
    public void PageInitialization()
    {
        BasePage = new BasePage(Driver);
        DashboardPage = new DashboardPage(Driver);
    }

    [Test]
    public void ValidateSearchFunctionality()
    {
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));

        var firstOption = BasePage.SearchMainMenu("Time");
        var expectedFirstOption = "Time";
        Assert.That(expectedFirstOption, Is.EqualTo(firstOption));

        BasePage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }

    [Test]
    public void ValidateDashboardAccess()
    {
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));

        DashboardPage.NavigateMainMenu("Dashboard");

        var expectedDashboardTitles = new List<string>() { "Time at Work", "My Actions", "Quick Launch" };
        var dashboardTitles = DashboardPage.GetDashBoardElementsTitles();

       foreach (var title in expectedDashboardTitles)
        {
            Assert.That(dashboardTitles.Contains(title), Is.True, $"Title {title} is not in dashboard titles list: {dashboardTitles}");
        }
    }
}