namespace BlogRest.Dtos;

public class ArticleProfileDto
{
    public DateTimeOffset PostDate { get; init; }
    public string Title { get; init; } = "";
    public string SubTitle { get; init; } = "";
}