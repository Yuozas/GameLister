using GameLister.Models.Accounts;

namespace GameLister.Extensions;

public static class GamesOwnerExtensions
{
    public static IEnumerable<string> GetSortedInfo(this SimilarGamesOwner[] similarGamesOwners)
    {
        var sortedBySimilarityGames = similarGamesOwners
            .SelectMany(gameOwner => gameOwner.Games.Select(game => (game, gameOwner.Account)))
            .OrderBy(tuple => tuple.game.Similarity);
        foreach (var (SimilarGame, Account) in sortedBySimilarityGames)
            yield return $"{Account.Name}, {Math.Round(SimilarGame.Similarity * 100, 2)}%".FillUpToMinimum(25) + $" - {SimilarGame.Game.Name}";
    }
}
