using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFramework.Extensions;

public static class WebElemetExtension
{
    public static void ClearAndEnterText(this IWebElement element, string text)
    {
        element.Clear();
        element.SendKeys(text);
    }

    public static void SelectFromDropDownByText(this IWebElement el, string text)
    {
        var select = new SelectElement(el);
        select.SelectByText(text);
    }

    public static void SelectFromDropDownByValue(this IWebElement el, string value)
    {
        var select = new SelectElement(el);
        select.SelectByValue(value);
    }

    public static void SelectFromDropDownByIndex(this IWebElement el, int index)
    {
        var select = new SelectElement(el);
        select.SelectByIndex(index);
    }

    public static string GetSelectedDropDownValue(this IWebElement el)
    {
        var select = new SelectElement(el);
        return select.SelectedOption.Text;
    }
}
