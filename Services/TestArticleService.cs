using BlogRest.Entities;

namespace BlogRest.Services;

public class TestArticleService : IArticleService
{
    public IEnumerable<Article> GetAllArticles()
    {
        List<Article> articles = new();
        Article article = new();
        articles.Add(article);
        return articles;
    }
}