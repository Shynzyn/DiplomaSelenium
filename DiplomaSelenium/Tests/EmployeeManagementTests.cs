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

        Assert.True(kpiFound, $"kpi wasn't found");

        PerformancePage.DeleteKpi(kpiName);

        kpiFound = PerformancePage.SearchKpi(kpiName, jobTitle);

        Assert.False(kpiFound, $"kpi was not deleted successfully");
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
        Assert.That(AssignedSuccessfuly, "Leave wasn't assigned successfully");
    }

    [Test]
    public void SearchEmployee()
    {
        PimPage.NavigateMainMenu("PIM");
        _createdEmployeeName = PimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        var isEmployeeFound = PimPage.SearchEmployee(_createdEmployeeName);
        Assert.That(isEmployeeFound, Is.True, "Employee was not found");

        isEmployeeFound = PimPage.SearchEmployee("1234567");
        Assert.That(isEmployeeFound, Is.False, "Employee was not deleted");

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
        Assert.That(isUpdatedEmployeeFound, Is.True, $"Updated employee: {updatedEmployeeName} was not found");

        PimPage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }

    [Test]
    public void DeleteEmployee()
    {
        PimPage.NavigateMainMenu("PIM");
        _createdEmployeeName = PimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        var isEmployeeFound = PimPage.SearchEmployee(_createdEmployeeName);
        Assert.That(isEmployeeFound, Is.True, $"Employee: {_createdEmployeeName} was not found");

        PimPage.DeleteRecord(_createdEmployeeName);
        isEmployeeFound = PimPage.SearchEmployee(_createdEmployeeName);
        Assert.That(isEmployeeFound, Is.False, $"Employee: {_createdEmployeeName} was not deleted");

        PimPage.LogOut();
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoginPage));
    }

    [Test]
    public void ValidateCandidateManagementInRecruitmentFunctionality()
    {
        RecruitmentPage.NavigateMainMenu("Recruitment");

        var firstNameWithId = RecruitmentPage.ModifyWithRandomId(Constants.EmployeeName);
        RecruitmentPage.AddCandidate(firstNameWithId, Constants.EmployeeLastName, Constants.Vacancy, Constants.Email);

        RecruitmentPage.NavigateTopNavBar("Candidates");

        RecruitmentPage.SearchCandidate(firstNameWithId);
        var isCandidateFound = RecruitmentPage.CheckIfRecordFound(Constants.Vacancy);
        Assert.That(isCandidateFound, Is.True, $"Candidate {firstNameWithId} was not found");

        RecruitmentPage.DeleteRecord(Constants.Vacancy);
        RecruitmentPage.SearchCandidate(firstNameWithId);

        isCandidateFound = RecruitmentPage.CheckIfRecordFound(Constants.Vacancy);
        Assert.That(isCandidateFound, Is.False, $"Candidate {firstNameWithId} was not deleted");
    }

    [Test]
    public void ValidateAssignSkillToEmployeeProfile()
    {
        PimPage.NavigateMainMenu("PIM");
        var employeeNameWithId = PimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);
        var employeeFound = PimPage.SearchEmployee(employeeNameWithId);
        Assert.That(employeeFound, $"Employee {employeeNameWithId} was not found");

        PimPage.EditRecord(employeeNameWithId);
        PimPage.AddSkill(Constants.SkillName, Constants.YearsOfExp);

        PimPage.SearchEmployee(employeeNameWithId);
        PimPage.EditRecord(employeeNameWithId);
        var isSkillAssigned = PimPage.CheckIfSkillAssigned(Constants.SkillName);
        Assert.That(isSkillAssigned, Is.True, $"Skill was not assigned to employe: {employeeNameWithId}");
    }
}