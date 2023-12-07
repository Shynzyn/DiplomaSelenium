using Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaSelenium.Tests;

public abstract class BaseTest
{
    protected IWebDriver _driver;
    private LoginPage _loginPage;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _driver = BrowserFactory.GetDriver(BrowserType.Chrome);
        _driver.Manage().Window.Maximize();
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
        _driver.ClearLocalStorage();
        _driver.Navigate().GoToUrl(SiteUrls.OrangeDemoLogout);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        BrowserFactory.CloseDriver();
    }
}