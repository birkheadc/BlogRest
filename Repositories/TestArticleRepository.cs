using BlogRest.Contexts;
using BlogRest.Dtos;
using BlogRest.Models;

namespace BlogRest.Repositories;

public class TestArticleRepository : IArticleRepository
{
    private readonly IArticleContext context;
    private readonly ArticleConverter converter;

    public TestArticleRepository(IArticleContext context, ArticleConverter converter)
    {
        this.context = context;
        this.converter = converter;
    }

    public IEnumerable<ArticleDto> GetAllArticles()
    {
        IEnumerable<Article> articles = context.FindAll();
        IEnumerable<ArticleDto> dtos = converter.EntityToDtoIEnum(articles);
        return dtos;
    }

    public void CreateNewArticle(ArticleDto dto)
    {
        Article article = converter.DtoToEntity(dto);
        CreateNewArticle(article);
    }

    public void CreateNewArticle(Article article)
    {
        context.Add(article);
    }

    public void CreateNewArticle(string title, string subtitle, string body)
    {
        Article article = new()
        {
            Id = Guid.NewGuid(),
            PostDate = DateTimeOffset.Now,
            Title = title,
            SubTitle = subtitle,
            Body = body
        };
        CreateNewArticle(article);
    }
}