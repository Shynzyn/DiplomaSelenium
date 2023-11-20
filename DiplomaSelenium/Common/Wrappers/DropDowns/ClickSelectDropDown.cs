using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.DropDowns;

public class ClickSelectDropDown : BaseDropDown
{
    public ClickSelectDropDown(By by) : base(by)
    {
        By = by;
    }

    public new void SelectByText(string text)
    {
        WebElement.Click();

        var xpathSring = $"//div[@role='listbox']/div[contains(., '{text}')]";
        var option = new BaseWebElement(By.XPath(xpathSring));
        option.Click();
    }
}
