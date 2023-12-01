using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using DiplomaSelenium.Common.Wrappers.InputFields;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class AdminPage : BasePage
{
    private BaseButton _nationalitiesButton = new(By.XPath("//nav[@aria-label='Topbar Menu']//li[contains(., 'Nationalities')]"));
    private ClickSelectDropDown _userRoleDropDown = new(By.XPath("//label[text()='User Role']/../following-sibling::div"));
    private ClickSelectDropDown _statusDropDown = new(By.XPath("//label[text()='Status']/../following-sibling::div"));
    private BaseInputField _jobTitleField = new(By.XPath("//label[text()='Job Title']/../..//input"));
    private BaseInputField _usernameField = new(By.XPath("//label[text()='Username']/../..//input"));
    private BaseInputField _passwordField = new(By.XPath("//label[text()='Password']/../..//input"));
    private BaseInputField _passwordRepeatField = new(By.XPath("//label[text()='Confirm Password']/../..//input"));
    private SuggestionInputField _employeeNameField = new(By.XPath("//label[text()='Employee Name']/../..//input"));

    public AdminPage(IWebDriver driver) : base(driver)
    {
        Driver = driver;
    }

    public void ChangeNationality(string nationality, string nationalityEditted)
    {
        _nationalitiesButton.Click();
        var nationalityRecord = new RecordElement(By.XPath($"//div[@class='oxd-table-card']/div/div/div[contains(., '{nationality}')]/../.."));
        nationalityRecord.ClickEditRecord();

        var nationalityNameInput = new BaseInputField(By.XPath("//label[contains(., 'Name')]/../following-sibling::div/input"));
        nationalityNameInput.ForceEnterText(nationalityEditted);
        SubmitButton.Click();
        SuccessToaster.WaitTillGone();
    }

    public void AddJobTitle(string jobTitle)
    {
        AddButton.Click();
        _jobTitleField.SendKeys(jobTitle);
        SubmitButton.Click();
        SuccessToaster.WaitTillGone();
    }

    public void CreateAdminUser(string username, string password)
    {
        AddButton.Click();
        _userRoleDropDown.SelectByText("Admin");
        _statusDropDown.SelectByText("Enabled");
        _employeeNameField.EnterText("a");
        _usernameField.EnterText(username);
        _passwordField.EnterText(password);
        _passwordRepeatField.EnterText(password);
        SubmitButton.Click();
        SuccessToaster.WaitTillGone();
    }

    public void SearchAdminUser(string username)
    {
        _usernameField.EnterText(username);
        SubmitButton.Click();

    }
}
