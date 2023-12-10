using DiplomaSelenium.Common;
using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class PerformancePage : BasePage
{
    private BaseButton _addButton = new(By.XPath("//button[contains(., 'Add')]"));
    private BaseInputField _kpiField = new(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
    private ClickSelectDropDown _jobTitleDropDown = new(By.XPath("//label[contains(., 'Job Title')]/../following-sibling::div"));

    public void AddNewKpi(string kpiName, string jobTitle)
    {
        NavigateTopNavBar(TopBarNavConstants.Configure, TopBarNavConstants.Kpi);
        _addButton.Click();
        _kpiField.SendKeys(kpiName);
        _jobTitleDropDown.SelectByText(jobTitle);
        SubmitButton.Click();
        SuccessToaster.WaitTillGone();
    }

    public bool SearchKpi(string kpiName, string JobTitle)
    {
        NavigateTopNavBar(TopBarNavConstants.Configure, TopBarNavConstants.Kpi);
        _jobTitleDropDown.SelectByText(JobTitle);
        SubmitButton.Click();

        var kpiFound = CheckIfRecordFound(kpiName);

        return kpiFound;
    }

    public void DeleteKpi(string kpiName)
    {
        DeleteRecord(kpiName);
    }
}
