namespace BlogRest.Models;

public record BlogSettings
{
    public string BlogTitle { get; init; } = "";
    public string BlogSubTitle { get; init; } = "";
}