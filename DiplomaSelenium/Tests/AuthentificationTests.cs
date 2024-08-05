using DiplomaSelenium.Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;

namespace DiplomaSelenium.Tests;

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
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));
    }

    [Test]
    public void ValidateSuccessfulLogout()
    {
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));
        _loginPage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }
}