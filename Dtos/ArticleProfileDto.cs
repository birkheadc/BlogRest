namespace BlogRest.Dtos;

public record ArticleProfileDto
{
    public long PostDateUnixTimeSeconds { get; init; }
    public string Title { get; init; } = "";
    public string SubTitle { get; init; } = "";
}