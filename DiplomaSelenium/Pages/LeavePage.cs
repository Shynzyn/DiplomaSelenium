using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using DiplomaSelenium.Common.Wrappers.InputFields;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class LeavePage : BasePage
{
    private ClickSelectDropDown _leaveTypeDropDown = new ClickSelectDropDown(By.XPath("//label[contains(., 'Leave Type')]/../following-sibling::div/div"));
    private SuggestionInputField _employeeNameField = new SuggestionInputField(By.XPath("//label[contains(., 'Employee Name')]/../following-sibling::div//input"));
    private BaseInputField _entitlementField = new BaseInputField(By.XPath("//label[contains(., 'Entitlement')]/../following-sibling::div/input"));
    private BaseInputField _fromDateField = new BaseInputField(By.XPath("//label[contains(., 'From Date')]/../following-sibling::div//input"));
    private BaseInputField _toDateField = new BaseInputField(By.XPath("//label[contains(., 'To Date')]/../following-sibling::div//input"));

    public void AddEntitlement(string employeeName, int daysCredit)
    {
        _employeeNameField.EnterText(employeeName);
        _leaveTypeDropDown.SelectByText("CAN - Vacation");
        _entitlementField.EnterText(daysCredit.ToString());
        _submitButton.Click();
        _confirmButton.Click();
    }

    public bool AssignLeave(string employeeName)
    {
        var dateStringFormula = "yyyy-MM-dd";
        var fromDate = DateTime.Today.AddDays(7).ToString(dateStringFormula);
        var toDate = DateTime.Today.AddDays(14).ToString(dateStringFormula);

        _employeeNameField.EnterText(employeeName);
        _leaveTypeDropDown.SelectByText("CAN - Vacation");
        _fromDateField.EnterText(fromDate.ToString());
        _toDateField.EnterText(toDate.ToString());
        _toDateField.Click();
        _toDateField.ForceEnterText(toDate.ToString());
        _submitButton.Click();
        bool assignedSuccess = _successToaster.Displayed;
        return assignedSuccess;
    }
}
