using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.InputFields;

internal class SuggestionInputField : BaseInputField
{
    public SuggestionInputField(By by) : base(by)
    {
        By = by;
    }

    public override void EnterText(string text)
    {
        WebElement!.Click();
        WebElement.Clear();
        WebElement.SendKeys(text);

        var xpathString = $"//span[contains(., '{text}')]";
        var dropDownOption = new BaseWebElement(By.XPath($"//div[@role='listbox']{xpathString}"));
        dropDownOption.Click();
    }

    public override void ForceEnterText(string text)
    {
        WebElement!.Click();
        WebElement.SendKeys(Keys.Control + "a" + Keys.Delete);
        WebElement.SendKeys(text);

        var xpathString = $"//span[contains(., '{text}')]";
        var dropDownOption = new BaseWebElement(By.XPath($"//div[@role='listbox']{xpathString}"));
        dropDownOption.Click();
    }
}
