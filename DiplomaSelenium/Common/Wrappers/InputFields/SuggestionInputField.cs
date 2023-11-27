using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.InputFields;

internal class SuggestionInputField : BaseInputField
{
    public SuggestionInputField(By by) : base(by)
    {
        By = by;
    }

    public void EnterText(string text)
    {
        WebElement.Click();
        WebElement.Clear();
        WebElement.SendKeys(text);

        var xpathString = $"//span[contains(., '{text}')]";
        var dropDownOption = new BaseWebElement(By.XPath($"//div[@role='listbox']{xpathString}"));
        dropDownOption.Click();
    }
}
