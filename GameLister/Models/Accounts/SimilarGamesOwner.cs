using GameLister.Models.Games;

namespace GameLister.Models.Accounts;

public readonly struct SimilarGamesOwner
{
    public readonly Account Account { get; init; }
    public readonly SimilarGame[] Games { get; init; }
}
