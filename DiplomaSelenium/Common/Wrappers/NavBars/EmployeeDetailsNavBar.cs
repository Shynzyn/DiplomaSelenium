using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers.NavBars;
public class EmployeeDetailsNavBar : BaseWebElement
{
    public EmployeeDetailsNavBar(By by = null) : base(by)
    {
        By = By.XPath($"//div[@class='orangehrm-tabs']");
    }

    public void Navigate(string tabName)
    {
        var mainOptionXpathString = $"{By.Criteria}//*[text()='{tabName}']";
        var option = new BaseWebElement(By.XPath(mainOptionXpathString));
        option.Click();
    }
}
