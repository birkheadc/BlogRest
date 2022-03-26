using BlogRest.Dtos;
using BlogRest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogRest.Contexts;

public interface IArticleContext
{
    public IEnumerable<Article> FindAll();
    public void Add(Article article);
    public IEnumerable<ArticleProfileDto> FindAllArticleProfilesByPostDateDesc();
    public IEnumerable<ArticleProfileDto> FindNRecentProfiles(int n);
    public Article FindByTitle(string title);
}