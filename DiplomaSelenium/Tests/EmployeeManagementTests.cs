using Common;
using DiplomaSelenium.Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;

namespace DiplomaSelenium.Tests;

public class EmployeeManagementTests : BaseTest
{
    private string _createdEmployeeName;

    [OneTimeSetUp]
    public void PageInitialization()
    {
        PimPage = new PimPage(Driver);
        PerformancePage = new PerformancePage(Driver);
        RecruitmentPage = new RecruitmentPage(Driver);
    }

    [Test]
    public void ValidateAddNewEmployee()
    {
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));

        PimPage.NavigateMainMenu("PIM");
        _createdEmployeeName = PimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        var employeeIsFound = PimPage.SearchEmployee(_createdEmployeeName);

        Assert.That(employeeIsFound, $"Employee {_createdEmployeeName} was not found");
    }

    [Test]
    public void ValidatePerformanceManagementFunctionality()
    {
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));

        PerformancePage.NavigateMainMenu("Performance");

        var randomId = new Random().Next(10000, 99999);
        var kpiName = "PowerfullKPI" + randomId;
        var jobTitle = "QA Lead";

        PerformancePage.AddNewKpi(kpiName, jobTitle);
        var kpiFound = PerformancePage.SearchKpi(kpiName, jobTitle);

        Assert.True(kpiFound);

        PerformancePage.DeleteKpi(kpiName);

        kpiFound = PerformancePage.SearchKpi(kpiName, jobTitle);

        Assert.False(kpiFound);
    }

    [Test]
    public void ValidateRecruitmentManagementFunctionality()
    {
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));

        RecruitmentPage.NavigateMainMenu("Recruitment");

        RecruitmentPage.NavigateTopNavBar("Vacancies");
    }
}
