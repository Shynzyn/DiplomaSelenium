using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class PerformancePage : BasePage
{
    private BaseButton _configureButton = new(By.XPath("(//span[@class='oxd-topbar-body-nav-tab-item'])[1]"));
    private BaseButton _addButton = new(By.XPath("//button[contains(., 'Add')]"));
    private BaseDropDown _configureDropDown = new(By.XPath("//ul[@class='oxd-dropdown-menu']"));
    private BaseInputField _kpiField = new(By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]"));
    private ClickSelectDropDown _jobTitleDropDown = new(By.XPath("//label[contains(., 'Job Title')]/../following-sibling::div"));

    public void AddNewKpi(string kpiName, string jobTitle)
    {
        NavigateToConfigureKpi();
        _addButton.Click();
        _kpiField.SendKeys(kpiName);
        _jobTitleDropDown.SelectByText(jobTitle);
        _submitButton.Click();
        _successToaster.WaitTillGone();
    }

    public bool SearchKpi(string kpiName, string JobTitle)
    {
        NavigateToConfigureKpi();
        _jobTitleDropDown.SelectByText(JobTitle);
        _submitButton.Click();

        var kpiFound = CheckIfRecordFound(kpiName);

        return kpiFound;
    }

    public void DeleteKpi(string kpiName)
    {
        DeleteRecord(kpiName);
    }

    private void NavigateToConfigureKpi()
    {
        _configureButton.Click();
        _configureDropDown.SelectByText("KPI");
    }
}
