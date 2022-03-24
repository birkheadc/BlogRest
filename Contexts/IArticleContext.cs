using BlogRest.Dtos;
using BlogRest.Models;

namespace BlogRest.Contexts;

public interface IArticleContext
{
    public IEnumerable<Article> FindAll();
    public bool Add(Article article);
    public IEnumerable<ArticleProfileDto> FindAllArticleProfilesByPostDateDesc();
    public Article FindByTitle(string title);
}