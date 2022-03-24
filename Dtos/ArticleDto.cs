namespace BlogRest.Dtos;

public record ArticleDto
{
    public Guid Id { get; init; }
    public long PostDateUnixTimeSeconds { get; init; }
    public string Title { get; init; } = "";
    public string SubTitle { get; init; } = "";
    public string Body { get; init; } = "";
}