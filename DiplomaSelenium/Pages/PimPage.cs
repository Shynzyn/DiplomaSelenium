using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.InputFields;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class PimPage : BasePage
{
    private BaseButton _addEmployee = new(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));
    private BaseInputField _firstNameField = new(By.XPath("//input[@name='firstName']"));
    private BaseInputField _lastNameField = new(By.XPath("//input[@name='lastName']"));
    private BaseButton _employeeList = new(By.XPath("//nav[@aria-label='Topbar Menu']/ul/li[2]/a"));
    private SuggestionInputField _employeeNameField = new(By.XPath("//label[contains(., 'Employee Name')]/../following-sibling::div//input"));

    public PimPage(IWebDriver driver) : base(driver)
    {
        Driver = driver;
    }

    public string AddNewEmployee(string firstName, string lastName, int id = 0)
    {
        _addEmployee.Click();

        var randomId = new Random().Next(10000, 99999);
        var firstNameWithId = firstName + randomId;

        _firstNameField.SendKeys(firstNameWithId);
        _lastNameField.SendKeys(lastName);

        SubmitButton.Click();
        SuccessToaster.WaitTillGone();

        return firstNameWithId;
    }

    public bool SearchEmployee(string firstName)
    {
        _employeeList.Click();
        try
        {
            _employeeNameField.EnterText(firstName);
            SubmitButton.Click();
            var IsEmployeeFound = CheckIfRecordFound(firstName);
            return IsEmployeeFound;
        }
        catch
        {
            return false;
        }
    }

    public string EditEmployeeName(string employeeFirstName, string updatedFirstName)
    {
        EditRecord(employeeFirstName);
        _firstNameField.ForceEnterText(updatedFirstName);
        SubmitButton.Click();
        return updatedFirstName;
    }
}
