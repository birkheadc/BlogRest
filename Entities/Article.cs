namespace BlogRest.Entities;

public record Article
{
    public Guid Id { get; set; }
    public DateTimeOffset PostDate { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Body { get; set; }

}