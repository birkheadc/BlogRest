namespace BlogRest.Dtos;

public record InboundArticleDto
{
    public string Title { get; init; } = "";
    public string SubTitle { get; init; } = "";
    public string Body { get; init; } = "";
}