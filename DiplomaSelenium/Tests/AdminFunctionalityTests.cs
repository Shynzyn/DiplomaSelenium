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
}