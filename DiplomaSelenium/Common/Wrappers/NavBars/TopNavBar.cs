using DiplomaSelenium.Common.Wrappers.DropDowns;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.NavBars;

public class TopNavBar : BaseDropDown
{
    private readonly string _dropDownOptionXpath = "/ul//a[contains(., '{0}')]";

    public TopNavBar(By by = null) : base(by)
    {
        By = By.XPath($"//nav[@aria-label='Topbar Menu']");
    }

    public bool CheckIfOptionExist(string mainOption, string dropDownOption = "")
    {
        var mainOptionXpathString = $"{By.Criteria}//li[contains(., '{mainOption}')]";
        var option = new BaseWebElement(By.XPath(mainOptionXpathString));
        if (dropDownOption == "")
        {
            return option.Displayed;
        }
        else
        {
            option.Click();
            var dropDownOptionString = string.Format(_dropDownOptionXpath, dropDownOption);
            var dropDownOptionXpath = mainOptionXpathString + dropDownOptionString;
            var dropDown = new BaseWebElement(By.XPath(dropDownOptionXpath));
            return dropDown.Displayed;
        }
    }

    public void Navigate(string mainOption, string dropDownOption = "")
    {
        var mainOptionXpathString = $"{By.Criteria}//li[contains(., '{mainOption}')]";
        var option = new BaseWebElement(By.XPath(mainOptionXpathString));
        option.Click();

        if (dropDownOption != "")
        {
            var dropDownOptionString = string.Format(_dropDownOptionXpath, dropDownOption);
            var dropDownOptionXpath = mainOptionXpathString + dropDownOptionString;
            var dropDown = new BaseWebElement(By.XPath(dropDownOptionXpath));
            dropDown.Click();
        }
    }
}