using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers;

public class BaseInputField : BaseWebElement
{
    public BaseInputField(By by) : base(by)
    {
    }

    public void EnterText(string text)
    {
        WebElement.Click();
        WebElement.Clear();
        WebElement.SendKeys(text);
    }

    public void ForceEnterText(string text)
    {
        WebElement.Click();
        WebElement.SendKeys(Keys.Control + "a" + Keys.Delete);
        WebElement.SendKeys(text);
    }
}