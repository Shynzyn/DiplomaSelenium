﻿using Common;
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
        LeavePage = new LeavePage(Driver);
    }

    [Test]
    public void ValidateAddNewEmployee()
    {
        PimPage.NavigateMainMenu("PIM");
        _createdEmployeeName = PimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        var employeeIsFound = PimPage.SearchEmployee(_createdEmployeeName);

        Assert.That(employeeIsFound, $"Employee {_createdEmployeeName} was not found");
    }

    [Test]
    public void ValidatePerformanceManagementFunctionality()
    {
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
        var vacancyName = "AQA Engineer";
        var jobTitle = "QA Engineer";
        var hiringManager = "m";

        var randomId = new Random().Next(10000, 99999);
        var vacancyNameWithRandomId = vacancyName + randomId;

        RecruitmentPage.NavigateMainMenu("Recruitment");
        RecruitmentPage.AddNewVacancy(vacancyNameWithRandomId, jobTitle, hiringManager);

        RecruitmentPage.NavigateTopNavBar("Vacancies");
        RecruitmentPage.SearchVacancy(jobTitle);

        var IsRecordFound = RecruitmentPage.CheckIfRecordFound(vacancyNameWithRandomId);
        Assert.That(IsRecordFound, Is.True, $"Vacancy was not found");

        RecruitmentPage.DeleteRecord(vacancyNameWithRandomId);
        RecruitmentPage.SearchVacancy(jobTitle);

        IsRecordFound = RecruitmentPage.CheckIfRecordFound(vacancyNameWithRandomId);
        Assert.That(IsRecordFound, Is.False, $"Vacancy was not deleted");
    }

    [Test]
    public void AssignLeave()
    {
        LeavePage.NavigateMainMenu("PIM");
        _createdEmployeeName = PimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        LeavePage.NavigateMainMenu("Leave");
        LeavePage.NavigateTopNavBar("Entitlements", "Add Entitlements");
        LeavePage.AddEntitlement(_createdEmployeeName, 50);

        LeavePage.NavigateMainMenu("Leave");
        LeavePage.NavigateTopNavBar("Assign Leave");
        var AssignedSuccessfuly = LeavePage.AssignLeave(_createdEmployeeName);
        Assert.That(AssignedSuccessfuly);
    }

    [Test]
    public void SearchEmployee()
    {
        PimPage.NavigateMainMenu("PIM");
        _createdEmployeeName = PimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        var isEmployeeFound = PimPage.SearchEmployee(_createdEmployeeName);
        Assert.That(isEmployeeFound, Is.True);

        isEmployeeFound = PimPage.SearchEmployee("1234567");
        Assert.That(isEmployeeFound, Is.False);

        PimPage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }

    [Test]
    public void EditEmployeeDetails()
    {
        PimPage.NavigateMainMenu("PIM");
        _createdEmployeeName = PimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        PimPage.SearchEmployee(_createdEmployeeName);
        var updatedEmployeeName = PimPage.EditEmployeeName(_createdEmployeeName, "Bob33123");

        var isUpdatedEmployeeFound = PimPage.SearchEmployee(updatedEmployeeName);
        Assert.That(isUpdatedEmployeeFound, Is.True);

        PimPage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }

    [Test]
    public void DeleteEmployee()
    {
        PimPage.NavigateMainMenu("PIM");
        _createdEmployeeName = PimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        var isEmployeeFound = PimPage.SearchEmployee(_createdEmployeeName);
        Assert.That(isEmployeeFound, Is.True);

        PimPage.DeleteRecord(_createdEmployeeName);
        isEmployeeFound = PimPage.SearchEmployee(_createdEmployeeName);
        Assert.That(isEmployeeFound, Is.False);

        PimPage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }
}