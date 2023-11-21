using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers;

internal class RecordElement : BaseWebElement
{
    public RecordElement(By by) : base(by)
    {
    }

    public void DeleteRecord()
    {
        var deleteRecordIcon = new BaseWebElement(By.XPath($"{By.Criteria}//button/i[@class='oxd-icon bi-trash']"));
        deleteRecordIcon.Click();

        var yesDeleteButton = new BaseWebElement(By.XPath("//button[contains(.,'Yes, Delete')]"));
        yesDeleteButton.Click();
    }

    public void ClickEditRecord()
    {
        var editRecordIcon = new BaseWebElement(By.XPath($"{By.Criteria}//button/i[@class='oxd-icon bi-pencil-fill']"));
        editRecordIcon.Click();
    }
}
