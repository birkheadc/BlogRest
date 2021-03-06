using BlogRest.Dtos;
using BlogRest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogRest.Services;

public interface IArticleService
{
    public IEnumerable<ArticleDto> GetAllArticles();
    public IEnumerable<ArticleProfileDto> GetAllArticleProfiles();
    public IEnumerable<ArticleProfileDto> GetNRecentArticleProfiles(int n);
    public void CreateNewArticle(string title, string subtitle, string body);
    public void CreateNewArticle(Article article);
    public void CreateNewArticle(InboundArticleDto inboundArticleDto);

    public ArticleDto GetArticleByTitle(string title);

    public void CreateTestArticles();
}