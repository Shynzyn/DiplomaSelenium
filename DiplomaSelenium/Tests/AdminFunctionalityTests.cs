using Common;
using DiplomaSelenium.Common;
using DiplomaSelenium.Pages;
using NUnit.Framework;

namespace DiplomaSelenium.Tests;

public class AdminFunctionalityTests : BaseTest
{
    [OneTimeSetUp]
    public void PageInitialization()
    {
        AdminPage = new AdminPage(Driver);
        PimPage = new PimPage(Driver);
    }

    [Test]
    public void ValidateAdminFunction()
    {
        Assert.That(Driver.Url, Is.EqualTo(SiteUrls.OrangeDemoLoggedInDashboardPage));

        AdminPage.NavigateMainMenu("Admin");

        var usersOptionExist = AdminPage.CheckTopNavBarOptionExist("User Management", "Users");
        Assert.That(usersOptionExist, Is.True, "Unable to locate User Management > Users");

        var jobTitlesOptionExist = AdminPage.CheckTopNavBarOptionExist("Job", "Job Titles");
        Assert.That(jobTitlesOptionExist, Is.True, "Unable to locate Job > Job Titles");
    }

    [Test]
    public void EditNationality()
    {
        AdminPage.NavigateMainMenu("Admin");

        var nationalityBefore = "Albanian";
        var nationalityAfter = "AlbanianLT";

        AdminPage.ChangeNationality(nationalityBefore, nationalityAfter);

        var nationalityEdittedSuccessfully = AdminPage.CheckIfRecordFound(nationalityAfter);
        Assert.That(nationalityEdittedSuccessfully, Is.True, $"Failed to change nationality from {nationalityBefore}, to {nationalityAfter}");

        AdminPage.ChangeNationality(nationalityAfter, nationalityBefore);
        nationalityEdittedSuccessfully = AdminPage.CheckIfRecordFound(nationalityBefore);
        Assert.That(nationalityEdittedSuccessfully, Is.True, $"Failed to revert nationality back to original");
    }

    [Test]
    public void AddJobTitle()
    {
        AdminPage.NavigateMainMenu("Admin");
        AdminPage.NavigateTopNavBar("Job", "Job Titles");

        var jobTitleWithRandomId = AdminPage.ModifyWithRandomId(Constants.JobTitle);
        AdminPage.AddJobTitle(jobTitleWithRandomId);

        var isJobTitleCreatedSuccessfuly = AdminPage.CheckIfRecordFound(jobTitleWithRandomId);
        Assert.That(isJobTitleCreatedSuccessfuly, Is.True, $"Job title: {jobTitleWithRandomId} was not found in job titles list");
    }

    [Test]
    public void SearchAdmin()
    {
        AdminPage.NavigateMainMenu("Admin");

        var usernameWithId = AdminPage.ModifyWithRandomId(Constants.UserAdminUsername);

        AdminPage.CreateAdminUser(usernameWithId, Constants.Password);
        AdminPage.NavigateTopNavBar("User Management", "Users");

        AdminPage.SearchAdminUser(usernameWithId);

        var isRecordFound = AdminPage.CheckIfRecordFound(usernameWithId);
        Assert.That(isRecordFound, Is.True);
    }

    [Test]
    public void ResetPassword()
    {

        AdminPage.NavigateMainMenu("Admin");

        var usernameWithId = AdminPage.ModifyWithRandomId(Constants.UserAdminUsername);

        AdminPage.CreateAdminUser(usernameWithId, Constants.Password);

        AdminPage.LogOut();

        var message = LoginPage.ResetPassowrd(usernameWithId);
        var expectedMessage = "Reset Password link sent successfully";
        Assert.That(message == expectedMessage);
    }

    [Test]
    public void ValidateJobTitlesManagementFunctionality()
    {
        AdminPage.NavigateMainMenu("Admin");
        AdminPage.NavigateTopNavBar("Job", "Job Titles");

        var jobTitleWithRandomId = AdminPage.ModifyWithRandomId(Constants.JobTitle);
        AdminPage.AddJobTitle(jobTitleWithRandomId);

        var jobTitleExist = AdminPage.CheckIfRecordFound(jobTitleWithRandomId);
        Assert.That(jobTitleExist, Is.True, $"Job title: {jobTitleWithRandomId} was not found in job titles list");

        AdminPage.DeleteRecord(jobTitleWithRandomId);
        AdminPage.NavigateTopNavBar("Job", "Job Titles");
        jobTitleExist = AdminPage.CheckIfRecordFound(jobTitleWithRandomId);
        Assert.That(jobTitleExist, Is.False, $"Job title: {jobTitleWithRandomId} was not deleted successfully");
    }

    [Test]
    public void ValidateAddCustomFieldsToEmployeeProfile()
    {
        PimPage.NavigateMainMenu("PIM");
        PimPage.NavigateTopNavBar("Configuration", "Custom Fields");

        var customFieldNameWithId = PimPage.ModifyWithRandomId(Constants.CustomFieldName);
        PimPage.AddCustomField(customFieldNameWithId, Constants.CustomFieldCategory);

        var customFieldExist = AdminPage.CheckIfRecordFound(customFieldNameWithId);
        Assert.That(customFieldExist, Is.True, $"Custom Field: {customFieldNameWithId} was not created successfully");

        PimPage.NavigateTopNavBar("Employee List");
        var employeeNameWithId = PimPage.AddNewEmployee(Constants.EmployeeName, Constants.EmployeeLastName);

        PimPage.SearchEmployee(employeeNameWithId);
        PimPage.EditRecord(employeeNameWithId);
        PimPage.UpdateCustomFieldText(customFieldNameWithId, Constants.CustomFieldCategory, "Dog");

        PimPage.NavigateTopNavBar("Employee List");
        PimPage.SearchEmployee(employeeNameWithId);
        PimPage.EditRecord(employeeNameWithId);
        var customFieldText = PimPage.GetCustomFieldText(customFieldNameWithId, Constants.CustomFieldCategory);
        Assert.That(customFieldText == Constants.CustomFieldText);
    }
}