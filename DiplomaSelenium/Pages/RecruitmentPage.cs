using DiplomaSelenium.Common.Wrappers;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using DiplomaSelenium.Common.Wrappers.InputFields;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class RecruitmentPage : BasePage
{
    private BaseInputField _vacancyNameField = new BaseInputField(By.XPath("//label[contains(., 'Vacancy Name')]/../following-sibling::div/input"));
    private ClickSelectDropDown _jobTitleDropDown = new ClickSelectDropDown(By.XPath("//label[contains(., 'Job Title')]/../following-sibling::div/div"));
    private SuggestionInputField _hiringManagerField = new SuggestionInputField(By.XPath("//label[contains(., 'Hiring Manager')]/../following-sibling::div//input"));
    public RecruitmentPage(IWebDriver driver) : base(driver)
    {
        Driver = driver;
    }

    public void AddNewVacancy(string vacancyName, string jobTitle, string hiringManager)
    {
        NavigateTopNavBar("Vacancies");
        AddButton.Click();
        _vacancyNameField.EnterText(vacancyName);
        _jobTitleDropDown.SelectByText(jobTitle);
        _hiringManagerField.EnterText(hiringManager);
        SubmitButton.Click();
    }

    public void SearchVacancy(string jobTitle)
    {
        NavigateTopNavBar("Vacancies");
        _jobTitleDropDown.SelectByText(jobTitle);
        SubmitButton.Click();
    }
}
