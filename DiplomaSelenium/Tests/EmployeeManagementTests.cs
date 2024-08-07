﻿using DiplomaSelenium.Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;

namespace DiplomaSelenium.Tests;

[Parallelizable(ParallelScope.Fixtures)]
public class EmployeeManagementTests : BaseTest
{
    private PimPage _pimPage;
    private PerformancePage _performancePage;
    private RecruitmentPage _recruitmentPage;
    private LeavePage _leavePage;
    private string _createdEmployeeName;

    [OneTimeSetUp]
    public void PageInitialization()
    {
        _pimPage = new PimPage();
        _performancePage = new PerformancePage();
        _recruitmentPage = new RecruitmentPage();
        _leavePage = new LeavePage();
    }

    [Test]
    public void ValidateAddNewEmployee()
    {
        _pimPage.NavigateMainMenu(MenuNavConstants.Pim);
        _createdEmployeeName = _pimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        var employeeIsFound = _pimPage.SearchEmployee(_createdEmployeeName);

        Assert.That(employeeIsFound, $"Employee {_createdEmployeeName} was not found");
    }

    [Test]
    public void ValidatePerformanceManagementFunctionality()
    {
        _performancePage.NavigateMainMenu(MenuNavConstants.Performance);
        var kpiName = Constants.Kpi.ModifyWithRandomId();

        _performancePage.AddNewKpi(kpiName, Constants.KpiJobTitle);
        var kpiFound = _performancePage.SearchKpi(kpiName, Constants.KpiJobTitle);

        Assert.That(kpiFound, Is.True, $"kpi wasn't found");

        _performancePage.DeleteKpi(kpiName);

        kpiFound = _performancePage.SearchKpi(kpiName, Constants.KpiJobTitle);

        Assert.That(kpiFound, Is.False, $"kpi was not deleted successfully");
    }

    [Test]
    public void ValidateRecruitmentManagementFunctionality()
    {
        var vacancyName = "AQA Engineer";
        var jobTitle = "QA Engineer";
        var hiringManager = "m";

        var vacancyNameWithRandomId = vacancyName.ModifyWithRandomId();
        _recruitmentPage.NavigateMainMenu(MenuNavConstants.Recruitment);
        _recruitmentPage.AddNewVacancy(vacancyNameWithRandomId, jobTitle, hiringManager);

        _recruitmentPage.NavigateTopNavBar(TopBarNavConstants.Vacancies);
        _recruitmentPage.SearchVacancy(jobTitle);

        var IsRecordFound = _recruitmentPage.CheckIfRecordFound(vacancyNameWithRandomId);
        Assert.That(IsRecordFound, Is.True, $"Vacancy was not found");

        _recruitmentPage.DeleteRecord(vacancyNameWithRandomId);
        _recruitmentPage.SearchVacancy(jobTitle);

        IsRecordFound = _recruitmentPage.CheckIfRecordFound(vacancyNameWithRandomId);
        Assert.That(IsRecordFound, Is.False, $"Vacancy was not deleted");
    }

    [Test]
    public void AssignLeave()
    {
        _leavePage.NavigateMainMenu(MenuNavConstants.Pim);
        _createdEmployeeName = _pimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        _leavePage.NavigateMainMenu(MenuNavConstants.Leave);
        _leavePage.NavigateTopNavBar(TopBarNavConstants.Entitlements, TopBarNavConstants.AddEntitlements);
        _leavePage.AddEntitlement(_createdEmployeeName, 50);

        _leavePage.NavigateMainMenu(MenuNavConstants.Leave);
        _leavePage.NavigateTopNavBar(TopBarNavConstants.AssignLeave);
        var assignedSuccessfuly = _leavePage.AssignLeave(_createdEmployeeName);
        Assert.That(assignedSuccessfuly, "Leave wasn't assigned successfully");
    }

    [Test]
    public void SearchEmployee()
    {
        _pimPage.NavigateMainMenu(MenuNavConstants.Pim);
        _createdEmployeeName = _pimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        var isEmployeeFound = _pimPage.SearchEmployee(_createdEmployeeName);
        Assert.That(isEmployeeFound, Is.True, "Employee was not found");

        isEmployeeFound = _pimPage.SearchEmployee("1234567");
        Assert.That(isEmployeeFound, Is.False, "Employee was not deleted");

        _pimPage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }

    [Test]
    public void EditEmployeeDetails()
    {
        _pimPage.NavigateMainMenu(MenuNavConstants.Pim);
        _createdEmployeeName = _pimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        _pimPage.SearchEmployee(_createdEmployeeName);
        var updatedEmployeeName = _pimPage.EditEmployeeName(_createdEmployeeName, "Bob33123");

        var isUpdatedEmployeeFound = _pimPage.SearchEmployee(updatedEmployeeName);
        Assert.That(isUpdatedEmployeeFound, Is.True, $"Updated employee: {updatedEmployeeName} was not found");

        _pimPage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }

    [Test]
    public void DeleteEmployee()
    {
        _pimPage.NavigateMainMenu(MenuNavConstants.Pim);
        _createdEmployeeName = _pimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        var isEmployeeFound = _pimPage.SearchEmployee(_createdEmployeeName);
        Assert.That(isEmployeeFound, Is.True, $"Employee: {_createdEmployeeName} was not found");

        _pimPage.DeleteRecord(_createdEmployeeName);
        isEmployeeFound = _pimPage.SearchEmployee(_createdEmployeeName);
        Assert.That(isEmployeeFound, Is.False, $"Employee: {_createdEmployeeName} was not deleted");

        _pimPage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }

    [Test]
    public void ValidateCandidateManagementInRecruitmentFunctionality()
    {
        _recruitmentPage.NavigateMainMenu(MenuNavConstants.Recruitment);

        var firstNameWithId = Constants.EmployeeName.ModifyWithRandomId();
        _recruitmentPage.AddCandidate(firstNameWithId, Constants.EmployeeLastName, Constants.Vacancy, Constants.Email);

        _recruitmentPage.NavigateTopNavBar(TopBarNavConstants.Candidates);

        _recruitmentPage.SearchCandidate(firstNameWithId);
        var isCandidateFound = _recruitmentPage.CheckIfRecordFound(Constants.Vacancy);
        Assert.That(isCandidateFound, Is.True, $"Candidate {firstNameWithId} was not found");

        _recruitmentPage.DeleteRecord(Constants.Vacancy);
        _recruitmentPage.SearchCandidate(firstNameWithId);

        isCandidateFound = _recruitmentPage.CheckIfRecordFound(Constants.Vacancy);
        Assert.That(isCandidateFound, Is.False, $"Candidate {firstNameWithId} was not deleted");
    }

    [Test]
    public void ValidateAssignSkillToEmployeeProfile()
    {
        _pimPage.NavigateMainMenu(MenuNavConstants.Pim);
        var employeeNameWithId = _pimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);
        var employeeFound = _pimPage.SearchEmployee(employeeNameWithId);
        Assert.That(employeeFound, $"Employee {employeeNameWithId} was not found");

        _pimPage.EditRecord(employeeNameWithId);
        _pimPage.AddSkill(Constants.SkillName, Constants.YearsOfExp);

        _pimPage.SearchEmployee(employeeNameWithId);
        _pimPage.EditRecord(employeeNameWithId);
        var isSkillAssigned = _pimPage.CheckIfSkillAssigned(Constants.SkillName);
        Assert.That(isSkillAssigned, Is.True, $"Skill was not assigned to employe: {employeeNameWithId}");
    }
}