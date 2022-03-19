using BlogRest.Entities;

namespace BlogRest.Repositories;

public interface IArticleRepository
{
    public IEnumerable<Article> FindAll();
}