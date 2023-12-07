using Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;

namespace DiplomaSelenium.Tests;

[Parallelizable(ParallelScope.Fixtures)]
public class AuthentificationTests : BaseTest
{
    private LoginPage _loginPage;

    [OneTimeSetUp]
    public void PageInitialization()
    {
        _loginPage = new LoginPage();
    }

    [Test]
    public void ValidateSuccessfulLogin()
    {
        Assert.That(_driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));
    }

    [Test]
    public void ValidateSuccessfulLogout()
    {
        Assert.That(_driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));
        _loginPage.LogOut();
        Assert.That(_driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }
}