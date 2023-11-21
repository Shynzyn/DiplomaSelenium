using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.DropDowns;

public class TopNavBarDropDown : BaseDropDown
{

    //public TopNavBarDropDown(string dropDownTitle)
    //{
    //    By = By.XPath($"//nav[@aria-label='Topbar Menu']//li[contains(., '{dropDownTitle}')]");
    //}

    public TopNavBarDropDown(By by) : base(by)
    {
    }

    public bool CheckIfOptionExist(string optionText)
    {
        WebElement.Click();
        var xpathString = $"/ul//a[contains(., '{optionText}')]";
        var option = new BaseWebElement(By.XPath($"{By.Criteria}{xpathString}"));
        return option.Displayed;
    }
}
