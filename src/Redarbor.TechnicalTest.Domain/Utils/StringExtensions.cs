namespace Redarbor.TechnicalTest.Domain.Utils;

public static class StringExtensions
{
    public static string SanitizeString(this string number)
    {
        if (string.IsNullOrWhiteSpace(number)) return string.Empty;

        string onlyNumbers = Regex.Replace(number, @"[^\d]", "");

        return onlyNumbers.Trim();
    }
}
