using Common;
using Common.Wrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSelenium.Common.Wrappers;

public class BaseDropDown : BaseWebElement
{
    public BaseDropDown(By by) : base(by)
    {
        By = by;
    }

    public void SelectByText(string text)
    {
        var xpathString = $"//a[contains(., '{text}')]";
        var dropDownOption = new BaseWebElement(By.XPath($"{By.Criteria}{xpathString}"));
        dropDownOption.Click();
    }
}
