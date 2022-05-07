namespace GameLister.Extensions;

public static class Levenshtein
{
    /// <summary>
    ///     Calculate percentage similarity of two strings
    ///     <param name="source">Source String to Compare with</param>
    ///     <param name="target">Targeted String to Compare</param>
    ///     <returns>Return Similarity between two strings from 0 to 1.0</returns>
    /// </summary>
    public static double CalculateSimilarity(this string source, string target)
    {
        if (source is null || target is null)
            return 0;
        if (source.Length == 0 || target.Length == 0)
            return 0;
        if (source == target)
            return 1;

        int stepsToSame = ComputeLevenshteinDistance(source, target);
        return 1 - (stepsToSame / (double)Math.Max(source.Length, target.Length));
    }

    /// <summary>
    ///     Returns the number of steps required to transform the source string
    ///     into the target string.
    /// </summary>
    public static int ComputeLevenshteinDistance(this string source, string target, bool ignoreCase = true)
    {
        if (source is null || target is null)
            return 0;
        if (source.Length == 0 || target.Length == 0)
            return 0;
        if (source == target)
            return 1;

        var sourceWordCount = source.Length;
        var targetWordCount = target.Length;

        var distance = new int[sourceWordCount + 1, targetWordCount + 1];

        for (var x = 0; x <= sourceWordCount; distance[x, 0] = x++);
        for (var y = 0; y <= targetWordCount; distance[0, y] = y++);
        for (var x = 1; x <= sourceWordCount; x++)
        {
            for (var y = 1; y <= targetWordCount; y++)
            {
                var comparison = ignoreCase ? target[y - 1].CompareIgnoreCase(source[x - 1]) : target[y - 1] == source[x - 1];
                var cost = comparison ? 0 : 1;
                distance[x, y] = Math.Min(Math.Min(distance[x - 1, y] + 1, distance[x, y - 1] + 1), distance[x - 1, y - 1] + cost);
            }
        }

        return distance[sourceWordCount, targetWordCount];
    }
}
