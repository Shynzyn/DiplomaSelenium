using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers;

public class BaseInputField : BaseWebElement
{
    public BaseInputField(By by) : base(by)
    {
        By = by;
    }
}