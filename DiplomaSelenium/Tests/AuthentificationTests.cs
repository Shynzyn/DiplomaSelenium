using Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaSelenium.Tests;

public class AuthentificationTests : BaseTest
{
    [OneTimeSetUp]
    public void PageInitialization()
    {
        LoginPage = new LoginPage(Driver);
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
        LoginPage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }

}
