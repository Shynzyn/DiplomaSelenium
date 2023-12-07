namespace DiplomaSelenium.Common;
public static class HelperMethods
{
    public static string ModifyWithRandomId(this string text)
    {
        var randomId = new Random().Next(10000, 99999);
        return text + randomId;
    }
}
