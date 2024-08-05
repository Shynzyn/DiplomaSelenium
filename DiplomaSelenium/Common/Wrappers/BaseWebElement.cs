using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.Drawing;

namespace DiplomaSelenium.Common.Wrappers;

public class BaseWebElement : IWebElement
{
    protected IWebDriver? Driver => BrowserFactory.WebDriver;
    protected By By { get; set; }
    protected IWebElement? WebElement => Driver?.GetWait(5).Until(ExpectedConditions.ElementIsVisible(By));

    public BaseWebElement(By by)
    {
        By = by;
    }

    public IWebElement FindElement(By by) => Driver?.FindElement(by)!;

    public string GetClassName() => WebElement!.GetAttribute("class");

    public void WaitTillGone() => Driver?.GetWait().Until(ExpectedConditions.StalenessOf(WebElement));

    public string TagName => WebElement!.TagName;

    public string Text => WebElement!.Text;

    public string Value => WebElement!.GetAttribute("value");

    public bool Selected => WebElement!.Selected;

    public Point Location => WebElement!.Location;

    public Size Size => WebElement!.Size;

    public bool Displayed => Driver!.FindElements(By).Any();

    public bool Enabled => WebElement!.Enabled;

    public void Clear()
    {
        WebElement!.Clear();
    }

    public void Click()
    {
        WebElement!.Click();
    }

    public void SendKeys(string text)
    {
        WebElement!.SendKeys(text);
    }

    public void Submit()
    {
        if (WebElement != null && WebElement.TagName.ToLower() == "form")
        {
            WebElement.Submit();
        }
        else
        {
            var parentForm = WebElement?.FindElement(By.XPath("./ancestor::form"))!;

            if (parentForm != null)
            {
                parentForm.Submit();
            }
            else
            {
                throw new InvalidOperationException("The element is not a form element or does not have a parent form.");
            }
        }
    }

    public string GetAttribute(string attributeName) => WebElement!.GetAttribute(attributeName);
    public string GetDomAttribute(string attributeName) => WebElement!.GetDomAttribute(attributeName);
    public string GetDomProperty(string propertyName) => WebElement!.GetDomProperty(propertyName);
    public string GetCssValue(string propertyName) => WebElement!.GetCssValue(propertyName);

    public ISearchContext GetShadowRoot()
    {
        if (WebElement != null)
        {
            try
            {
                var shadowRoot = (IWebElement)Driver!.GetJsExecutor().ExecuteScript("return arguments[0].shadowRoot;", WebElement);

                return shadowRoot;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Shadow DOM: {ex.Message}");
                throw;
            }
        }
        else
        {
            throw new InvalidOperationException("WebElement is null. Cannot retrieve Shadow DOM.");
        }
    }

    public ReadOnlyCollection<IWebElement> FindElements(By by) => Driver?.FindElements(by)!;
}