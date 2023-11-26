using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class AdminPage : BasePage
{
    private TopNavBarDropDown _userManagement = new (By.XPath("//nav[@aria-label='Topbar Menu']//li[contains(., 'User Management')]"));
    private TopNavBarDropDown _job = new (By.XPath("//nav[@aria-label='Topbar Menu']//li[contains(., 'Job')]"));
    private BaseButton _nationalitiesButton = new (By.XPath("//nav[@aria-label='Topbar Menu']//li[contains(., 'Nationalities')]"));

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
}
