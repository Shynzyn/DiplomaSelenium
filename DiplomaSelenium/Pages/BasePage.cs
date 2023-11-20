using Common.Wrappers;
using DiplomaSelenium.Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class BasePage
{
    protected IWebDriver Driver;
    protected BaseButton SubmitButton = new (By.XPath("//button[@type='submit']"));
    protected BaseWebElement ProfileNameSpan = new(By.XPath("//span[@class='oxd-userdropdown-tab']"));
    protected BaseDropDown ProfileDropDown = new(By.XPath("//ul[@class='oxd-dropdown-menu']"));
    protected BaseDropDown MainMenuDropDown = new(By.XPath("//ul[@class='oxd-main-menu']"));

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
}
