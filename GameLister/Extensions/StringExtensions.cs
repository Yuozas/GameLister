using System.Text.RegularExpressions;

namespace GameLister.Extensions;

public static class StringExtensions
{
    public static bool CompareIgnoreCase(this string text, string comparingText)
    {
        return string.Equals(text, comparingText, StringComparison.OrdinalIgnoreCase);
    }

    public static string MakeOnlySimpleWhitespace(this string text)
    {
        return Regex.Replace(text, @"\s+", " ");
    }

    public static bool AllWordsExist(this string searchText, string wordsString)
    {
        var words = wordsString.Split();
        var searchTextSingleSpaced = searchText.MakeOnlySimpleWhitespace();
        var searchWords = searchTextSingleSpaced.Split();
        foreach (var word in words)
            if (searchWords.All(searchWord => !searchWord.CompareIgnoreCase(word)))
                return false;
        return true;
    }
}
