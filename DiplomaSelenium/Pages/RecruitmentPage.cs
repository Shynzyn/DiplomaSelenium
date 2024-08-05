using DiplomaSelenium.Common;
using DiplomaSelenium.Common.Wrappers.DropDowns;
using DiplomaSelenium.Common.Wrappers.InputFields;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class RecruitmentPage : BasePage
{
    private BaseInputField _vacancyNameField = new(By.XPath("//label[contains(., 'Vacancy Name')]/../following-sibling::div/input"));
    private BaseInputField _firstNameField = new(By.XPath("//input[@name='firstName']"));
    private BaseInputField _lastNameField = new(By.XPath("//input[@name='lastName']"));
    private BaseInputField _emailField = new(By.XPath("//label[contains(., 'Email')]/../following-sibling::div/input"));
    private ClickSelectDropDown _jobTitleDropDown = new(By.XPath("//label[contains(., 'Job Title')]/../following-sibling::div/div"));
    private ClickSelectDropDown _vacancyDropDown = new(By.XPath("//label[text()='Vacancy']/..//following-sibling::div/div/div"));
    private SuggestionInputField _hiringManagerField = new(By.XPath("//label[contains(., 'Hiring Manager')]/../following-sibling::div//input"));
    private SuggestionInputField _candidateName = new(By.XPath("//label[contains(., 'Candidate Name')]/../following-sibling::div//input"));

    public void AddNewVacancy(string vacancyName, string jobTitle, string hiringManager)
    {
        NavigateTopNavBar(TopBarNavConstants.Vacancies);
        AddButton.Click();
        _vacancyNameField.EnterText(vacancyName);
        _jobTitleDropDown.SelectByText(jobTitle);
        _hiringManagerField.EnterText(hiringManager);
        SubmitButton.Click();
    }

    public void SearchVacancy(string jobTitle)
    {
        NavigateTopNavBar(TopBarNavConstants.Vacancies);
        _jobTitleDropDown.SelectByText(jobTitle);
        SubmitButton.Click();
    }

    public string AddCandidate(string firstName, string lastName, string vacancy, string email)
    {
        NavigateTopNavBar(TopBarNavConstants.Candidates);
        AddButton.Click();
        _firstNameField.EnterText(firstName);
        _lastNameField.EnterText(lastName);
        _vacancyDropDown.SelectByText(vacancy);
        _emailField.EnterText(email);
        SubmitButton.Click();

        return firstName;
    }

    public void SearchCandidate(string firstName)
    {
        try
        {
            _candidateName.ForceEnterText(firstName);
            SubmitButton.Click();
        }
        catch
        {
            Console.WriteLine("Candidate name was not present");
        }
    }
}
