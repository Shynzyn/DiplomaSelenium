using Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaSelenium.Tests;

public class BaseTest
{
    protected IWebDriver Driver;

    protected BasePage BasePage;
    protected LoginPage LoginPage;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Driver = BrowserFactory.GetDriver(BrowserType.Chrome);
        Driver.Manage().Window.Maximize();
    }

    [SetUp]
    public void SetUp()
    {
        LoginPage.LogIn();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Driver.ClearLocalStorage();
        BrowserFactory.CloseDriver();
    }
}