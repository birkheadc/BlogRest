namespace BlogRest.Models;

public record Article
{
    public Guid Id { get; init; }
    public DateTimeOffset PostDate { get; init; }
    public string Title { get; init; } = "";
    public string SubTitle { get; init; } = "";
    public string Body { get; init; } = "";
}