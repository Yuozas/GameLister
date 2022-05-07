namespace SteamAPIClient.Extensions;

public static class Extensions
{
    public static int ToInt(this bool @bool)
    {
        return @bool ? 1 : 0;
    }

    public static string ToIntString(this bool @bool)
    {
        return @bool.ToInt().ToString();
    }
}
