namespace BlogRest.Dtos;

public class ArticleProfileDto
{
    public long PostDateUnixTimeSeconds { get; init; }
    public string Title { get; init; } = "";
    public string SubTitle { get; init; } = "";
}