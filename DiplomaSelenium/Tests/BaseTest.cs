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
    protected PimPage PimPage;
    protected PerformancePage PerformancePage;
    protected AdminPage AdminPage;
    protected DashboardPage DashboardPage;
    protected RecruitmentPage RecruitmentPage;
    protected LeavePage LeavePage;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Driver = BrowserFactory.GetDriver(BrowserType.Chrome);
        Driver.Manage().Window.Maximize();
        LoginPage = new LoginPage(Driver);
    }

    [SetUp]
    public void SetUp()
    {
        LoginPage.LogIn();
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