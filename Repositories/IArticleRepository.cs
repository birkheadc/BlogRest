using BlogRest.Models;
using BlogRest.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlogRest.Repositories;

public interface IArticleRepository
{
    public IEnumerable<Article> FindAll();
    public void Add(Article article);
    public IEnumerable<ArticleProfileDto> FindAllArticleProfiles();
    public Article FindByTitle(string title);
}