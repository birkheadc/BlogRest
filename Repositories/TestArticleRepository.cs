using BlogRest.Entities;

namespace BlogRest.Repositories;

public class TestArticleRepository : IArticleRepository
{
    public IEnumerable<Article> FindAll()
    {
        throw new NotImplementedException();
    }
}