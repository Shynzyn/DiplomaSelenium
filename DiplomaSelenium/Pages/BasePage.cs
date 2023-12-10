using DiplomaSelenium.Common;
using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using DiplomaSelenium.Common.Wrappers.NavBars;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace DiplomaSelenium.Pages;

public class BasePage
{
    protected IWebDriver Driver;
    protected BaseButton SubmitButton = new(By.XPath("//button[@type='submit']"));
    protected BaseButton AddButton = new(By.XPath("//button[contains(., 'Add')]"));
    protected BaseButton ConfirmButton = new(By.XPath("//button[contains(., 'Confirm')]"));
    protected BaseWebElement ProfileNameSpan = new(By.XPath("//span[@class='oxd-userdropdown-tab']"));
    protected BaseDropDown ProfileDropDown = new(By.XPath("//ul[@class='oxd-dropdown-menu']"));
    protected BaseDropDown MainMenuDropDown = new(By.XPath("//ul[@class='oxd-main-menu']"));
    protected BaseWebElement SuccessToaster = new(By.XPath("//div[@id='oxd-toaster_1']"));
    protected BaseInputField MainMenuSearchField = new(By.XPath("//div[@class='oxd-main-menu-search']/input"));
    private readonly string _recordXpathBase = "//div[@class='oxd-table-card']//div[normalize-space()='{0}']/..";

    public BasePage()
    {
        Driver = BrowserFactory.WebDriver;
    }

    public void NavigateTo(string url)
    {
        Driver.Navigate().GoToUrl(url);
    }

    public void LogOut()
    {
        ProfileNameSpan.Click();
        ProfileDropDown.SelectByText("Logout");
    }

    public void NavigateMainMenu(string text)
    {
        MainMenuDropDown.SelectByText(text);
    }

    public void NavigateTopNavBar(string mainOption, string dropDownOption = "")
    {
        var topNavBar = new TopNavBar();
        topNavBar.Navigate(mainOption, dropDownOption);
    }

    public bool CheckTopNavBarOptionExist(string mainOption, string dropDownOption = "")
    {
        var topNavBar = new TopNavBar();
        return topNavBar.CheckIfOptionExist(mainOption, dropDownOption);
    }

    public string SearchMainMenu(string text)
    {
        MainMenuSearchField.SendKeys(text);
        var firstOption = MainMenuDropDown.GetOptionText(1);
        return firstOption;
    }

    public bool CheckIfRecordFound(string text)
    {
        var xpathString = string.Format(_recordXpathBase, text);
        try
        {
            Driver.GetWait(2).Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathString)));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void DeleteRecord(string text)
    {
        var xpathString = string.Format(_recordXpathBase, text);
        var foundRecord = new RecordElement(By.XPath(xpathString));
        foundRecord.DeleteRecord();
    }

    public void EditRecord(string text)
    {
        var xpathString = string.Format(_recordXpathBase, text);
        var foundRecord = new RecordElement(By.XPath(xpathString));
        foundRecord.ClickEditRecord();
    }
}