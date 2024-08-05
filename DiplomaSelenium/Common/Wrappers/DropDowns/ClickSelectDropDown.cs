using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.DropDowns;

public class ClickSelectDropDown : BaseDropDown
{
    public ClickSelectDropDown(By by) : base(by)
    {
    }

    public override void SelectByText(string text)
    {
        WebElement!.Click();

        var xpathString = $"//div[@role='listbox']/div[contains(., '{text}')]";
        var option = new BaseWebElement(By.XPath(xpathString));
        option.Click();
    }
}