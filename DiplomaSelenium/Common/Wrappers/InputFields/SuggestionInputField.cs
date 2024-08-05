using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.InputFields;

public class SuggestionInputField : BaseInputField
{
    private readonly string _xpathString = "//span[contains(., '{0}')]";
    private readonly string _xpathStringDropDownOption = "//div[@role='listbox']{0}";
    public SuggestionInputField(By by) : base(by)
    {
        By = by;
    }

    public override void EnterText(string text)
    {
        WebElement!.Click();
        WebElement.Clear();
        WebElement.SendKeys(text);

        var xpathStringFull = string.Format(_xpathString, text);
        var dropDownOptionXpathStringFull = string.Format(_xpathStringDropDownOption, xpathStringFull);
        var dropDownOption = new BaseWebElement(By.XPath(dropDownOptionXpathStringFull));
        dropDownOption.Click();
    }

    public override void ForceEnterText(string text)
    {
        WebElement!.Click();
        WebElement.SendKeys(Keys.Control + "a" + Keys.Delete);
        WebElement.SendKeys(text);

        var xpathStringFull = string.Format(_xpathString, text);
        var dropDownOptionXpathStringFull = string.Format(_xpathStringDropDownOption, xpathStringFull);
        var dropDownOption = new BaseWebElement(By.XPath(dropDownOptionXpathStringFull));
        dropDownOption.Click();
    }
}
