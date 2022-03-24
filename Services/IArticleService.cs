using BlogRest.Dtos;
using BlogRest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogRest.Services;

public interface IArticleService
{
    public IEnumerable<ArticleDto> GetAllArticles();
    public IEnumerable<ArticleProfileDto> GetAllArticleProfiles();
    public void CreateNewArticle(string title, string subtitle, string body);
    public void CreateNewArticle(ArticleDto dto);
    public void CreateNewArticle(Article article);

    public ArticleDto GetArticleByTitle(string title);
}