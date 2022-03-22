using BlogRest.Contexts;
using BlogRest.Dtos;
using BlogRest.Models;

namespace BlogRest.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly IArticleContext context;

    public ArticleRepository(IArticleContext context)
    {
        this.context = context;
    }

    public IEnumerable<Article> FindAll()
    {
        IEnumerable<Article> articles = context.FindAll();
        return articles;
    }

    public void Add(Article article)
    {
        context.Add(article);
    }

    public IEnumerable<ArticleProfileDto> FindAllArticleProfiles()
    {
        IEnumerable<ArticleProfileDto> profiles = context.FindAllArticleProfilesByPostDateDesc();
        return profiles;
    }

    public Article FindByTitle(string title)
    {
        Article article = context.FindByTitle(title);
        return article;
    }
}