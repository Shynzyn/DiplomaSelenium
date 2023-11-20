using Common;
using Common.Wrappers;
using OpenQA.Selenium;

namespace DiplomaSelenium.Common.Wrappers;

public class BaseButton : BaseWebElement
{
    public BaseButton(By by) : base(by)
    {
        By = by;
    }
}
