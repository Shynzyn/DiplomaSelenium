using Common;
using DiplomaSelenium.Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;

namespace DiplomaSelenium.Tests;

[Parallelizable(ParallelScope.Fixtures)]
public class AdminFunctionalityTests : BaseTest
{
    private AdminPage _adminPage;
    private PimPage _pimPage;
    private LoginPage _loginPage;

    [OneTimeSetUp]
    public void PageInitialization()
    {
        _adminPage = new AdminPage();
        _pimPage = new PimPage();
        _loginPage = new LoginPage();
    }

    [Test]
    public void ValidateAdminFunction()
    {
        Assert.That(_driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));

        _adminPage.NavigateMainMenu("Admin");

        var usersOptionExist = _adminPage.CheckTopNavBarOptionExist("User Management", "Users");
        Assert.That(usersOptionExist, Is.True, "Unable to locate User Management > Users");

        var jobTitlesOptionExist = _adminPage.CheckTopNavBarOptionExist("Job", "Job Titles");
        Assert.That(jobTitlesOptionExist, Is.True, "Unable to locate Job > Job Titles");
    }

    [Test]
    public void EditNationality()
    {
        _adminPage.NavigateMainMenu("Admin");

        var nationalityBefore = "Albanian";
        var nationalityAfter = "AlbanianLT";

        _adminPage.ChangeNationality(nationalityBefore, nationalityAfter);

        var nationalityEdittedSuccessfully = _adminPage.CheckIfRecordFound(nationalityAfter);
        Assert.That(nationalityEdittedSuccessfully, Is.True, $"Failed to change nationality from {nationalityBefore}, to {nationalityAfter}");

        _adminPage.ChangeNationality(nationalityAfter, nationalityBefore);
        nationalityEdittedSuccessfully = _adminPage.CheckIfRecordFound(nationalityBefore);
        Assert.That(nationalityEdittedSuccessfully, Is.True, $"Failed to revert nationality back to original");
    }

    [Test]
    public void AddJobTitle()
    {
        _adminPage.NavigateMainMenu("Admin");
        _adminPage.NavigateTopNavBar("Job", "Job Titles");

        var jobTitleWithRandomId = Constants.JobTitle.ModifyWithRandomId();
        _adminPage.AddJobTitle(jobTitleWithRandomId);

        var isJobTitleCreatedSuccessfuly = _adminPage.CheckIfRecordFound(jobTitleWithRandomId);
        Assert.That(isJobTitleCreatedSuccessfuly, Is.True, $"Job title: {jobTitleWithRandomId} was not found in job titles list");
    }

    [Test]
    public void SearchAdmin()
    {
        _adminPage.NavigateMainMenu("Admin");

        var usernameWithId = Constants.UserAdminUsername.ModifyWithRandomId();

        _adminPage.CreateAdminUser(usernameWithId, Constants.Password);
        _adminPage.NavigateTopNavBar("User Management", "Users");

        _adminPage.SearchAdminUser(usernameWithId);

        var isRecordFound = _adminPage.CheckIfRecordFound(usernameWithId);
        Assert.That(isRecordFound, Is.True);
    }

    [Test]
    public void ResetPassword()
    {
        _adminPage.NavigateMainMenu("Admin");

        var usernameWithId = Constants.UserAdminUsername.ModifyWithRandomId();

        _adminPage.CreateAdminUser(usernameWithId, Constants.Password);

        _adminPage.LogOut();

        var message = _loginPage.ResetPassword(usernameWithId);
        var expectedMessage = "Reset Password link sent successfully";
        Assert.That(message == expectedMessage);
    }

    [Test]
    public void ValidateJobTitlesManagementFunctionality()
    {
        _adminPage.NavigateMainMenu("Admin");
        _adminPage.NavigateTopNavBar("Job", "Job Titles");

        var jobTitleWithRandomId = Constants.JobTitle.ModifyWithRandomId();
        _adminPage.AddJobTitle(jobTitleWithRandomId);

        var jobTitleExist = _adminPage.CheckIfRecordFound(jobTitleWithRandomId);
        Assert.That(jobTitleExist, Is.True, $"Job title: {jobTitleWithRandomId} was not found in job titles list");

        _adminPage.DeleteRecord(jobTitleWithRandomId);
        _adminPage.NavigateTopNavBar("Job", "Job Titles");
        jobTitleExist = _adminPage.CheckIfRecordFound(jobTitleWithRandomId);
        Assert.That(jobTitleExist, Is.False, $"Job title: {jobTitleWithRandomId} was not deleted successfully");
    }

    [Test]
    public void ValidateAddCustomFieldsToEmployeeProfile()
    {
        _pimPage.NavigateMainMenu("PIM");
        _pimPage.NavigateTopNavBar("Configuration", "Custom Fields");

        var customFieldNameWithId = Constants.CustomFieldName.ModifyWithRandomId();
        _pimPage.AddCustomField(customFieldNameWithId, Constants.CustomFieldCategory);

        var customFieldExist = _adminPage.CheckIfRecordFound(customFieldNameWithId);
        Assert.That(customFieldExist, Is.True, $"Custom Field: {customFieldNameWithId} was not created successfully");

        _pimPage.NavigateTopNavBar("Employee List");
        var employeeNameWithId = _pimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        _pimPage.SearchEmployee(employeeNameWithId);
        _pimPage.EditRecord(employeeNameWithId);
        _pimPage.UpdateCustomFieldText(customFieldNameWithId, Constants.CustomFieldCategory, "Dog");

        _pimPage.NavigateTopNavBar("Employee List");
        _pimPage.SearchEmployee(employeeNameWithId);
        _pimPage.EditRecord(employeeNameWithId);
        var customFieldText = _pimPage.GetCustomFieldText(customFieldNameWithId, Constants.CustomFieldCategory);
        Assert.That(customFieldText == Constants.CustomFieldText, $"Custom Field Text {customFieldText} is not equal to {Constants.CustomFieldText}");
    }
}