using Common;
using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers;

internal class BaseInputField : BaseWebElement
{
    public BaseInputField(By by) : base(by)
    {
        By = by;
    }

}
