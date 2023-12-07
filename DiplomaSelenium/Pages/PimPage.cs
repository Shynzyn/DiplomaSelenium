using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using DiplomaSelenium.Common.Wrappers.InputFields;
using DiplomaSelenium.Common.Wrappers.NavBars;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class PimPage : BasePage
{
    private BaseButton _addEmployee = new(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary']"));
    private BaseInputField _firstNameField = new(By.XPath("//input[@name='firstName']"));
    private BaseInputField _lastNameField = new(By.XPath("//input[@name='lastName']"));
    private BaseButton _employeeList = new(By.XPath("//nav[@aria-label='Topbar Menu']/ul/li[2]/a"));
    private SuggestionInputField _employeeNameField = new(By.XPath("//label[contains(., 'Employee Name')]/../following-sibling::div//input"));
    private BaseInputField _customField = new(By.XPath("//label[contains(., 'Field Name')]/../following-sibling::div//input"));
    private ClickSelectDropDown _customFieldCategory = new(By.XPath("//label[contains(., 'Screen')]/../following-sibling::div/div/div"));
    private ClickSelectDropDown _customFieldType = new(By.XPath("//label[contains(., 'Type')]/../following-sibling::div/div/div"));
    private EmployeeDetailsNavBar _employeeDetailsNavBar = new();
    private ClickSelectDropDown _skillDropDown = new(By.XPath("//label[contains(., 'Skill')]/../following-sibling::div/div/div"));
    private BaseInputField _yearsOfExperienceField = new(By.XPath("//label[contains(., 'Years of Experience')]/../following-sibling::div//input"));
    private BaseInputField _commentField = new(By.XPath("//label[contains(., 'Comments')]/../following-sibling::div//textarea"));

    public string AddNewEmployee(string firstName, string lastName, int id = 0)
    {
        _addEmployee.Click();

        var randomId = new Random().Next(10000, 99999);
        var firstNameWithId = firstName + randomId;

        _firstNameField.SendKeys(firstNameWithId);
        _lastNameField.SendKeys(lastName);

        _submitButton.Click();
        _successToaster.WaitTillGone();

        return firstNameWithId;
    }

    public bool SearchEmployee(string firstName)
    {
        _employeeList.Click();
        try
        {
            _employeeNameField.EnterText(firstName);
            _submitButton.Click();
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
        _submitButton.Click();
        return updatedFirstName;
    }

    public void AddCustomField(string customFieldName, string category)
    {
        _addButton.Click();
        _customField.EnterText(customFieldName);
        _customFieldCategory.SelectByText(category);
        _customFieldType.SelectByText("Text or Number");
        _submitButton.Click();
        _successToaster.WaitTillGone();
    }

    private bool CheckIfCustomFieldExistInCategory(string customFieldName, string category)
    {
        Thread.Sleep(1000);
        var categoryTab = new BaseWebElement(By.XPath($"//div[@class='orangehrm-tabs']//a[contains(., '{category}')]"));
        var categoryClass = categoryTab.GetClassName();
        var activeClass = "orangehrm-tabs-item --active";

        if (categoryClass != activeClass)
        {
            categoryTab.Click();
        }

        var searchableField = _driver.FindElements(By.XPath($"//label[text()='{customFieldName}']/../following-sibling::div/input"));
        var doesFieldExist = searchableField.Any();
        return doesFieldExist;
    }

    public void UpdateCustomFieldText(string customFieldName, string category, string customFieldText)
    {
        var customFieldExist = CheckIfCustomFieldExistInCategory(customFieldName, category);

        if (customFieldExist)
        {
            var searchableField = new BaseInputField(By.XPath($"//label[text()='{customFieldName}']/../following-sibling::div/input"));
            searchableField.EnterText(customFieldText);

            var saveButton = new BaseButton(By.XPath("//h6[contains(.,'Custom Fields')]/..//button"));
            saveButton.Click();
        }
        else
        {
            throw new Exception($"Custom Field {customFieldName} was not found in {category}");
        }
    }

    public string GetCustomFieldText(string customFieldName, string category)
    {
        var customFieldExist = CheckIfCustomFieldExistInCategory(customFieldName, category);
        var customFieldText = "";

        if (customFieldExist)
        {
            var searchableField = new BaseInputField(By.XPath($"//label[text()='{customFieldName}']/../following-sibling::div/input"));
            customFieldText = searchableField.GetText();
            return customFieldText;
        }

        return customFieldText;
    }

    public void AddSkill(string skillName, int yearsExp, string comment = null)
    {
        _employeeDetailsNavBar.Navigate("Qualifications");
        var addSkillButton = new BaseButton(By.XPath("//h6[text()='Skills']/../button"));
        addSkillButton.Click();

        _skillDropDown.SelectByText(skillName);
        _yearsOfExperienceField.SendKeys(yearsExp.ToString());

        if (comment != null)
        {
            _commentField.EnterText(comment);
        }

        _submitButton.Click();
    }

    public bool CheckIfSkillAssigned(string skillName)
    {
        _employeeDetailsNavBar.Navigate("Qualifications");
        var isSkillFound = CheckIfRecordFound(skillName);
        return isSkillFound;
    }
}
