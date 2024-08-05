using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.InputFields;

public class BaseInputField : BaseWebElement
{
    public BaseInputField(By by) : base(by)
    {
    }

    public virtual void EnterText(string text)
    {
        WebElement!.Click();
        WebElement.Clear();
        WebElement.SendKeys(text);
    }

    public virtual void ForceEnterText(string text)
    {
        WebElement!.Click();
        WebElement.SendKeys(Keys.Control + "a" + Keys.Delete);
        WebElement.SendKeys(text);
    }

    public string GetText()
    {
        return WebElement!.GetAttribute("value");
    }
}