using DiplomaSelenium.Common.Wrappers.DropDowns;
using DiplomaSelenium.Common.Wrappers.InputFields;
using OpenQA.Selenium;

namespace DiplomaSelenium.Pages;

public class RecruitmentPage : BasePage
{
    private BaseInputField _vacancyNameField = new BaseInputField(By.XPath("//label[contains(., 'Vacancy Name')]/../following-sibling::div/input"));
    private BaseInputField _firstNameField = new BaseInputField(By.XPath("//input[@name='firstName']"));
    private BaseInputField _lastNameField = new BaseInputField(By.XPath("//input[@name='lastName']"));
    private BaseInputField _emailField = new BaseInputField(By.XPath("//label[contains(., 'Email')]/../following-sibling::div/input"));
    private ClickSelectDropDown _jobTitleDropDown = new ClickSelectDropDown(By.XPath("//label[contains(., 'Job Title')]/../following-sibling::div/div"));
    private ClickSelectDropDown _vacancyDropDown = new ClickSelectDropDown(By.XPath("//label[text()='Vacancy']/..//following-sibling::div/div/div"));
    private SuggestionInputField _hiringManagerField = new SuggestionInputField(By.XPath("//label[contains(., 'Hiring Manager')]/../following-sibling::div//input"));
    private SuggestionInputField _candidateName = new SuggestionInputField(By.XPath("//label[contains(., 'Candidate Name')]/../following-sibling::div//input"));

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

    public string AddCandidate(string firstName, string lastName, string vacancy, string email)
    {
        NavigateTopNavBar("Candidates");
        AddButton.Click();
        _firstNameField.EnterText(firstName);
        _lastNameField.EnterText(lastName);
        _vacancyDropDown.SelectByText(vacancy);
        _emailField.EnterText(email);
        SubmitButton.Click();

        return firstName;
    }

    public bool SearchCandidate(string firstName)
    {
        try
        {
            _candidateName.ForceEnterText(firstName);
            SubmitButton.Click();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
