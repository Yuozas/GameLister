using System.Text.RegularExpressions;

namespace GameLister.Extensions;

public static class StringExtensions
{
    public static bool CompareIgnoreCase(this string text, string comparingText)
    {
        return string.Equals(text, comparingText, StringComparison.OrdinalIgnoreCase);
    }

    public static bool CompareIgnoreCase(this char text, char comparingText)
    {
        return string.Equals(text.ToString(), comparingText.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public static string MakeOnlySimpleWhitespace(this string text)
    {
        return Regex.Replace(text, @"\s+", " ");
    }

    public static string LeaveAlphanumericsAndSpaces(this string text)
    {
        return Regex.Replace(text, @"[^a-zA-Z0-9 ]", " ");
    }

    public static bool AllWordsExist(this string searchText, string wordsString)
    {
        var words = wordsString.MakeOnlySimpleWhitespace().LeaveAlphanumericsAndSpaces().MakeOnlySimpleWhitespace().Split();
        var searchTextAdjusted = searchText.MakeOnlySimpleWhitespace().LeaveAlphanumericsAndSpaces().MakeOnlySimpleWhitespace();
        var searchWords = searchTextAdjusted.Split();
        foreach (var word in words)
            if (searchWords.All(searchWord => !searchWord.CompareIgnoreCase(word)))
                return false;
        return true;
    }

    public static string FillUpToMinimum(this string textToFill, int minCharAmount, char filler = ' ')
    {
        if (textToFill.Length >= minCharAmount)
            return textToFill;
        for (; textToFill.Length < minCharAmount; textToFill += filler) ;
        return textToFill;
    }
}
