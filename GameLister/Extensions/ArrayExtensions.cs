using System.Linq;

namespace GameLister.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] WrapToArray<T>(this T t)
        {
            return new T[] { t };
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
        {
            return enumerable is null || !enumerable.Any();
        }

        public static T[] ToArrayOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray() ?? Array.Empty<T>();
        }

        public static TSource? FirstOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource?, bool> predicate)
            where TSource : struct
        {
            return source.Cast<TSource?>().FirstOrDefault(predicate);
        }
    }
}
