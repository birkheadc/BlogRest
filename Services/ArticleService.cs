using BlogRest.Dtos;
using BlogRest.Models;
using BlogRest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogRest.Services;

public class ArticleService : IArticleService
{

    private readonly IArticleRepository repository;
    private readonly ArticleConverter converter;


    public ArticleService(IArticleRepository repository, ArticleConverter converter)
    {
        this.repository = repository;
        this.converter = converter;
    }
    public void CreateNewArticle(string title, string subtitle, string body)
    {
        Article article = new()
        {
            Id = Guid.NewGuid(),
            PostDate = DateTimeOffset.Now,
            Title = title,
            SubTitle = subtitle,
            Body = body
        };
        CreateNewArticle(article);
    }

    public void CreateNewArticle(ArticleDto dto)
    {
        Article article = converter.DtoToEntity(dto);
        CreateNewArticle(article);
    }

    public void CreateNewArticle(Article article)
    {
        //TODO: Use some kind of validation. For now, creating articles should be done directly to the database.
        //repository.Add(article);
    }

    public IEnumerable<ArticleDto> GetAllArticles()
    {
        IEnumerable<Article> articles = repository.FindAll();
        IEnumerable<ArticleDto> dtos = converter.EntityToDtoIEnum(articles);
        return dtos;
    }

    public IEnumerable<ArticleProfileDto> GetAllArticleProfiles()
    {
        IEnumerable<ArticleProfileDto> profiles = repository.FindAllArticleProfiles();
        return profiles;
    }

    public ArticleDto GetArticleByTitle(string title)
    {
        Article article = repository.FindByTitle(title);
        if (article == null)
        {
            return null;
        }
        ArticleDto dto = converter.EntityToDto(article);
        return dto;
    }
}