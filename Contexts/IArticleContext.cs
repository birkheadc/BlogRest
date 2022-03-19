using BlogRest.Dtos;
using BlogRest.Models;

namespace BlogRest.Contexts;

public interface IArticleContext
{
    public IEnumerable<Article> FindAll();
    public void Add(Article article);
}