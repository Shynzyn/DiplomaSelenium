using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class AdminPage : BasePage
{
    private BaseButton _nationalitiesButton = new(By.XPath("//nav[@aria-label='Topbar Menu']//li[contains(., 'Nationalities')]"));
    private ClickSelectDropDown _userRoleDropDown = new(By.XPath("//label[text()='User Role']/../following-sibling::div"));
    private BaseInputField _jobTitleField = new(By.XPath("//label[text()='Job Title']/../..//input"));

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
}
