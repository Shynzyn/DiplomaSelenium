using Common;
using Common.Wrappers;
using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace DiplomaSelenium.Pages;

public class BasePage
{
    protected IWebDriver Driver;
    protected BaseButton SubmitButton = new (By.XPath("//button[@type='submit']"));
    protected BaseWebElement ProfileNameSpan = new(By.XPath("//span[@class='oxd-userdropdown-tab']"));
    protected BaseDropDown ProfileDropDown = new(By.XPath("//ul[@class='oxd-dropdown-menu']"));
    protected BaseDropDown MainMenuDropDown = new(By.XPath("//ul[@class='oxd-main-menu']"));
    protected BaseWebElement SuccessToaster = new(By.XPath("//div[@id='oxd-toaster_1']"));
    protected BaseInputField MainMenuSearchField = new(By.XPath("//div[@class='oxd-main-menu-search']/input"));

    public BasePage(IWebDriver driver)
    {
        Driver = driver;
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

    public string SearchMainMenu(string text)
    {
        MainMenuSearchField.SendKeys(text);
        var firstOption = MainMenuDropDown.GetOptionText(1);
        return firstOption;
    }

    public bool CheckIfRecordsFound(string text)
    {
        var xpathString = $"//div[@class='oxd-table-card']/div/div/div[contains(., '{text}')]/../..";
        try
        {
            var element = Driver.GetWait(2).Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathString)));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void DeleteRecord(string text)
    {
        var foundRecord = new RecordElement(By.XPath($"//div[@class='oxd-table-card']/div/div/div[contains(., '{text}')]/../.."));
        foundRecord.DeleteRecord();
    }
}