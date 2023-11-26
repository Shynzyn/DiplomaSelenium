using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.DropDowns;

public class TopNavBarDropDown : BaseDropDown
{

    public TopNavBarDropDown(By by = null) : base(by)
    {
        By = By.XPath($"//nav[@aria-label='Topbar Menu']");
    }

    public bool CheckIfOptionExist(string mainOption, string dropDownOption = null)
    {
        var mainOptionXpathString = $"{By.Criteria}//li[contains(., '{mainOption}')]";
        var option = new BaseWebElement(By.XPath(mainOptionXpathString));
        if (dropDownOption == null)
        {
            return option.Displayed;
        }
        else
        {
            option.Click();
            var dropDownOptionXpath = mainOptionXpathString + $"/ul//a[contains(., '{dropDownOption}')]";
            var dropDown = new BaseWebElement(By.XPath(dropDownOptionXpath));
            return dropDown.Displayed;
        }
    }

    public void Navigate(string mainOption, string dropDownOption = null)
    {
        var mainOptionXpathString = $"{By.Criteria}//li[contains(., '{mainOption}')]";
        var option = new BaseWebElement(By.XPath(mainOptionXpathString));
        option.Click();

        if (dropDownOption != null)
        {
            var dropDownOptionXpath = mainOptionXpathString + $"/ul//a[contains(., '{dropDownOption}')]";
            var dropDown = new BaseWebElement(By.XPath(dropDownOptionXpath));
            dropDown.Click();
        }
    }
}
