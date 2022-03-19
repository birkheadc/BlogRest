using BlogRest.Dtos;
using BlogRest.Models;

namespace BlogRest.Repositories;

public interface IArticleRepository
{
    public IEnumerable<ArticleDto> GetAllArticles();
    public void CreateNewArticle(string title, string subtitle, string body);
    public void CreateNewArticle(ArticleDto dto);
    public void CreateNewArticle(Article article);
}