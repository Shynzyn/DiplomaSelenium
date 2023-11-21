using Common;
using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.DropDowns;

public class BaseDropDown : BaseWebElement
{
    public BaseDropDown(By by) : base(by)
    {
    }

    public string GetOptionText(int optionNumber)
    {
        var xpathString = $"/li[{optionNumber}]//span";
        var dropDownOption = new BaseWebElement(By.XPath($"{By.Criteria}{xpathString}"));
        var optionText = dropDownOption.Text;

        return optionText;
    }

    public void SelectByText(string text)
    {
        var xpathString = $"//a[contains(., '{text}')]";
        var dropDownOption = new BaseWebElement(By.XPath($"{By.Criteria}{xpathString}"));
        dropDownOption.Click();
    }
}
