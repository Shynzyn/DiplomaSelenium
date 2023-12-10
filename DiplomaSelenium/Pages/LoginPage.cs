using DiplomaSelenium.Common;
using DiplomaSelenium.Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class LoginPage : BasePage
{
    private BaseInputField _usernameField = new(By.XPath("//input[@name='username']"));
    private BaseInputField _passwordField = new(By.XPath("//input[@name='password']"));
    private BaseWebElement _forgotPasswordLink = new(By.XPath("//p[@class='oxd-text oxd-text--p orangehrm-login-forgot-header']"));
    private BaseWebElement _passwordLinkSentHeader = new(By.XPath("//h6[contains(., 'Reset Password link sent successfully')]"));

    public void LogIn()
    {
        NavigateTo(SiteUrls.OrangeDemoLoginPage);
        _usernameField.SendKeys(Constants.Username);
        _passwordField.SendKeys(Constants.Password);
        SubmitButton.Click();
    }

    public string ResetPassword(string username)
    {
        _forgotPasswordLink.Click();
        _usernameField.EnterText(username);
        SubmitButton.Click();
        var message = _passwordLinkSentHeader.Text;
        return message;
    }
}
