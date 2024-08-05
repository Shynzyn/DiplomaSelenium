using DiplomaSelenium.Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaSelenium.Tests;

public abstract class BaseTest
{
    protected IWebDriver Driver;
    private LoginPage _loginPage;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Driver = BrowserFactory.GetDriver(BrowserType.Chrome);
        Driver.Manage().Window.Maximize();
        _loginPage = new LoginPage();
    }

    [SetUp]
    public void SetUp()
    {
        _loginPage.LogIn();
    }

    [TearDown]
    public void TearDown()
    {
        Driver.ClearLocalStorage();
        Driver.Navigate().GoToUrl(SiteUrls.OrangeDemoLogout);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        BrowserFactory.CloseDriver();
    }
}