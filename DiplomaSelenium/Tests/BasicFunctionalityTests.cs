using Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;

namespace DiplomaSelenium.Tests;

[Parallelizable(ParallelScope.Fixtures)]
public class BasicFunctionalityTests : BaseTest
{
    private DashboardPage _dashboardPage;

    [OneTimeSetUp]
    public void PageInitialization()
    {
        _dashboardPage = new DashboardPage();
    }

    [Test]
    public void ValidateSearchFunctionality()
    {
        var firstOption = _dashboardPage.SearchMainMenu("Time");
        var expectedFirstOption = "Time";
        Assert.That(expectedFirstOption, Is.EqualTo(firstOption));

        _dashboardPage.LogOut();
        Assert.That(_driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }

    [Test]
    public void ValidateDashboardAccess()
    {
        _dashboardPage.NavigateMainMenu("Dashboard");

        var expectedDashboardTitles = new List<string>() { "Time at Work", "My Actions", "Quick Launch" };
        var dashboardTitles = _dashboardPage.GetDashBoardElementsTitles();

        foreach (var title in expectedDashboardTitles)
        {
            Assert.That(dashboardTitles.Contains(title), Is.True, $"Title {title} is not in dashboard titles list: {dashboardTitles}");
        }
    }
}