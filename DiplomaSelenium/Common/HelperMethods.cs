namespace Common;

public static class HelperMethods
{
    public static string GenerateKeyString(string key, int times)
    {
        return string.Concat(Enumerable.Repeat(key, times));
    }
}
