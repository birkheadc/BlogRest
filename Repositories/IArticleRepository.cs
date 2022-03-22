using BlogRest.Models;

namespace BlogRest.Repositories;

public interface IArticleRepository
{
    public IEnumerable<Article> GetAll();
    public void Add(Article article);
}