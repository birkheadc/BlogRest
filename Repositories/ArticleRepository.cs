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

    public IEnumerable<Article> GetAll()
    {
        IEnumerable<Article> articles = context.FindAll();
        return articles;
    }

    public void Add(Article article)
    {
        context.Add(article);
    }
}