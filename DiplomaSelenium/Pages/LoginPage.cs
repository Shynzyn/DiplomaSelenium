using DiplomaSelenium.Common.Wrappers;
using OpenQA.Selenium;
using DiplomaSelenium.Common;
using Common;

namespace DiplomaSelenium.Pages;

public class LoginPage : BasePage
{
    private BaseInputField _usernameField = new BaseInputField(By.XPath("//input[@name='username']"));
    private BaseInputField _passwordField = new BaseInputField(By.XPath("//input[@name='password']"));

    public LoginPage(IWebDriver driver) : base(driver)
    {
        Driver = driver;
    }

    public void LogIn()
    {
        NavigateTo(SiteUrls.OrangeDemoLoginPage);
        _usernameField.SendKeys(Constants.Username);
        _passwordField.SendKeys(Constants.Password);
        SubmitButton.Click();
    }
}
