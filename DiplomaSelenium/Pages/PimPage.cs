using DiplomaSelenium.Common;
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
    private readonly string _searchFieldXpathBase = "//label[text()='{0}']/../following-sibling::div/input";


    public string AddNewEmployee(string firstName, string lastName)
    {
        _addEmployee.Click();
        var firstNameWithId = firstName.ModifyWithRandomId();

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
            var isEmployeeFound = CheckIfRecordFound(firstName);
            return isEmployeeFound;
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

    public void AddCustomField(string customFieldName, string category)
    {
        AddButton.Click();
        _customField.EnterText(customFieldName);
        _customFieldCategory.SelectByText(category);
        _customFieldType.SelectByText("Text or Number");
        SubmitButton.Click();
        SuccessToaster.WaitTillGone();
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

        var searchableFieldString = string.Format(_searchFieldXpathBase, customFieldName);
        var searchableField = Driver.FindElements(By.XPath(searchableFieldString));
        var doesFieldExist = searchableField.Any();
        return doesFieldExist;
    }

    public void UpdateCustomFieldText(string customFieldName, string category, string customFieldText)
    {
        var customFieldExist = CheckIfCustomFieldExistInCategory(customFieldName, category);

        if (customFieldExist)
        {
            var searchableFieldString = string.Format(_searchFieldXpathBase, customFieldName);
            var searchableField = new BaseInputField(By.XPath(searchableFieldString));
            searchableField.EnterText(customFieldText);

            var saveButton = new BaseButton(By.XPath("//h6[contains(.,'Custom Fields')]/..//button"));
            saveButton.Click();
        }
        else
        {
            throw new Exception($"Custom Field {customFieldName} was not found in {category}");
        }
    }

    public string GetCustomFieldText(string customFieldName)
    {
        var searchableFieldString = string.Format(_searchFieldXpathBase, customFieldName);
        var searchableField = new BaseInputField(By.XPath(searchableFieldString));
        var customFieldText = searchableField.GetText();
        return customFieldText;
    }

    public void AddSkill(string skillName, int yearsExp, string comment = "")
    {
        _employeeDetailsNavBar.Navigate("Qualifications");
        var addSkillButton = new BaseButton(By.XPath("//h6[text()='Skills']/../button"));
        addSkillButton.Click();

        _skillDropDown.SelectByText(skillName);
        _yearsOfExperienceField.SendKeys(yearsExp.ToString());

        if (comment != "")
        {
            _commentField.EnterText(comment);
        }

        SubmitButton.Click();
    }

    public bool CheckIfSkillAssigned(string skillName)
    {
        _employeeDetailsNavBar.Navigate("Qualifications");
        var isSkillFound = CheckIfRecordFound(skillName);
        return isSkillFound;
    }
}
