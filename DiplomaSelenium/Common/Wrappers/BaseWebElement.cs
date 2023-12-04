using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Common.Wrappers;

public class BaseWebElement : IWebElement
{
    protected IWebDriver? Driver => BrowserFactory.WebDriver;
    protected By By { get; set; }
    protected IWebElement? WebElement
    {
        get
        {
            return Driver?.GetWait(5).Until(ExpectedConditions.ElementIsVisible(By));
        }
    }

    public BaseWebElement(By by)
    {
        By = by;
    }

    public IWebElement FindElement(By by)
    {
        return Driver?.FindElement(by)!;
    }

    public string GetClassName() => WebElement.GetAttribute("class");

    public void WaitTillGone() => Driver?.GetWait().Until(ExpectedConditions.StalenessOf(WebElement));

    public string TagName => WebElement.TagName;

    public string Text => WebElement.Text;

    public string Value => WebElement.GetAttribute("value");

    public bool Selected => WebElement.Selected;

    public Point Location => WebElement.Location;

    public Size Size => WebElement.Size;

    public bool Displayed => Driver.FindElements(By).Any();

    public bool Enabled => WebElement.Enabled;

    public void Clear()
    {
        WebElement.Clear();
    }

    public void Click()
    {
        WebElement.Click();
    }

    public void SendKeys(string text)
    {
        WebElement.SendKeys(text);
    }

    public void Submit()
    {
        throw new NotImplementedException();
    }

    public string GetAttribute(string attributeName)
    {
        throw new NotImplementedException();
    }

    public string GetDomAttribute(string attributeName)
    {
        throw new NotImplementedException();
    }

    public string GetDomProperty(string propertyName)
    {
        throw new NotImplementedException();
    }

    public string GetCssValue(string propertyName)
    {
        throw new NotImplementedException();
    }

    public ISearchContext GetShadowRoot()
    {
        throw new NotImplementedException();
    }

    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
        throw new NotImplementedException();
    }
}