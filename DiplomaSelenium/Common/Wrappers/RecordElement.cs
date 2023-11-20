using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers;

internal class RecordElement : BaseWebElement
{
    public RecordElement(By by) : base(by)
    {
        By = by;
    }

    public void DeleteRecord()
    {
        var deleteRecordIcon = new BaseWebElement(By.XPath($"{By.Criteria}//button[2]"));
        deleteRecordIcon.Click();

        var yesDeleteButton = new BaseWebElement(By.XPath("//button[contains(.,'Yes, Delete')]"));
        yesDeleteButton.Click();
    }
}
