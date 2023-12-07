using Common;
using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using DiplomaSelenium.Common.Wrappers.NavBars;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace DiplomaSelenium.Pages;

public class BasePage
{
    protected IWebDriver _driver;
    protected BaseButton _submitButton = new(By.XPath("//button[@type='submit']"));
    protected BaseButton _addButton = new(By.XPath("//button[contains(., 'Add')]"));
    protected BaseButton _confirmButton = new(By.XPath("//button[contains(., 'Confirm')]"));
    protected BaseWebElement _profileNameSpan = new(By.XPath("//span[@class='oxd-userdropdown-tab']"));
    protected BaseDropDown _profileDropDown = new(By.XPath("//ul[@class='oxd-dropdown-menu']"));
    protected BaseDropDown _mainMenuDropDown = new(By.XPath("//ul[@class='oxd-main-menu']"));
    protected BaseWebElement _successToaster = new(By.XPath("//div[@id='oxd-toaster_1']"));
    protected BaseInputField _mainMenuSearchField = new(By.XPath("//div[@class='oxd-main-menu-search']/input"));

    public BasePage()
    {
        _driver = BrowserFactory.WebDriver;
    }

    public void NavigateTo(string url)
    {
        _driver.Navigate().GoToUrl(url);
    }

    public void LogOut()
    {
        _profileNameSpan.Click();
        _profileDropDown.SelectByText("Logout");
    }

    public void NavigateMainMenu(string text)
    {
        _mainMenuDropDown.SelectByText(text);
    }

    public void NavigateTopNavBar(string mainOption, string dropDownOption = null)
    {
        var topNavBar = new TopNavBar();
        topNavBar.Navigate(mainOption, dropDownOption);
    }

    public bool CheckTopNavBarOptionExist(string mainOption, string dropDownOption = null)
    {
        var topNavBar = new TopNavBar();
        return topNavBar.CheckIfOptionExist(mainOption, dropDownOption);
    }

    public string SearchMainMenu(string text)
    {
        _mainMenuSearchField.SendKeys(text);
        var firstOption = _mainMenuDropDown.GetOptionText(1);
        return firstOption;
    }

    public bool CheckIfRecordFound(string text)
    {
        if (text == "")
        {
            return false;
        }
        var xpathString = $"//div[@class='oxd-table-card']//div[normalize-space()='{text}']/../..";
        try
        {
            _driver.GetWait(2).Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathString)));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void DeleteRecord(string text)
    {
        var foundRecord = new RecordElement(By.XPath($"//div[@class='oxd-table-card']//div[normalize-space()='{text}']/../.."));
        foundRecord.DeleteRecord();
    }

    public void EditRecord(string text)
    {
        var foundRecord = new RecordElement(By.XPath($"//div[@class='oxd-table-card']//div[normalize-space()='{text}']/../.."));
        foundRecord.ClickEditRecord();
    }
}