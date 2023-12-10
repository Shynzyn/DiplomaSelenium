using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using DiplomaSelenium.Common.Wrappers.InputFields;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class LeavePage : BasePage
{
    private ClickSelectDropDown _leaveTypeDropDown = new(By.XPath("//label[contains(., 'Leave Type')]/../following-sibling::div/div"));
    private SuggestionInputField _employeeNameField = new(By.XPath("//label[contains(., 'Employee Name')]/../following-sibling::div//input"));
    private BaseInputField _entitlementField = new(By.XPath("//label[contains(., 'Entitlement')]/../following-sibling::div/input"));
    private BaseInputField _fromDateField = new(By.XPath("//label[contains(., 'From Date')]/../following-sibling::div//input"));
    private BaseInputField _toDateField = new(By.XPath("//label[contains(., 'To Date')]/../following-sibling::div//input"));

    public void AddEntitlement(string employeeName, int daysCredit)
    {
        _employeeNameField.EnterText(employeeName);
        _leaveTypeDropDown.SelectByText("CAN - Vacation");
        _entitlementField.EnterText(daysCredit.ToString());
        SubmitButton.Click();
        ConfirmButton.Click();
    }

    public bool AssignLeave(string employeeName)
    {
        var dateStringFormula = "yyyy-MM-dd";
        var fromDate = DateTime.Today.AddDays(7).ToString(dateStringFormula);
        var toDate = DateTime.Today.AddDays(14).ToString(dateStringFormula);

        _employeeNameField.EnterText(employeeName);
        _leaveTypeDropDown.SelectByText("CAN - Vacation");
        _fromDateField.EnterText(fromDate);
        _toDateField.EnterText(toDate);
        _toDateField.Click();
        _toDateField.ForceEnterText(toDate);
        SubmitButton.Click();
        bool assignedSuccess = SuccessToaster.Displayed;
        return assignedSuccess;
    }
}
