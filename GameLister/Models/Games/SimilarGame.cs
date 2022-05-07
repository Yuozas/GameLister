namespace GameLister.Models.Games;

public readonly struct SimilarGame
{
    public readonly Game Game { get; init; }
    public readonly double Similarity { get; init; }
}