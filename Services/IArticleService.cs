using BlogRest.Dtos;
using BlogRest.Models;

namespace BlogRest.Services;

public interface IArticleService
{
    public IEnumerable<ArticleDto> GetAllArticles();
    public void CreateNewArticle(string title, string subtitle, string body);
    public void CreateNewArticle(ArticleDto dto);
    public void CreateNewArticle(Article article);
}