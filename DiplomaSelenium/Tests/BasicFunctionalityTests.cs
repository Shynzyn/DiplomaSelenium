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
    }

    [Test]
    public void ValidateSearchFunctionality()
    {
        var firstOption = BasePage.SearchMainMenu("Time");
        var expectedFirstOption = "Time";
        Assert.That(expectedFirstOption, Is.EqualTo(firstOption));

        BasePage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }
}
