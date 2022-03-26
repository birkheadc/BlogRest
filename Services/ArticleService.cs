using System.Text;
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

    public void CreateNewArticle(Article article)
    {
        repository.Add(article);
    }

    public void CreateNewArticle(InboundArticleDto inboundArticleDto)
    {
        CreateNewArticle(inboundArticleDto.Title, inboundArticleDto.SubTitle, inboundArticleDto.Body);
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

    public IEnumerable<ArticleProfileDto> GetNRecentArticleProfiles(int n)
    {
        IEnumerable<ArticleProfileDto> profiles = repository.FindNRecentProfiles(n);
        return profiles;
    }

    public ArticleDto GetArticleByTitle(string title)
    {
        try
        {
            Article article = repository.FindByTitle(title);
            ArticleDto dto = converter.EntityToDto(article);
            return dto;
        }
        catch(FileNotFoundException e)
        {
            throw e;
        }
    }

    public void CreateTestArticles()
    {
        int n = 10;
        StringBuilder builder = new();
        string s = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pulvinar mattis nunc sed blandit libero volutpat sed cras ornare. Et malesuada fames ac turpis egestas sed tempus urna et. Quis lectus nulla at volutpat diam. Et netus et malesuada fames ac turpis egestas integer eget. Dolor sit amet consectetur adipiscing. Metus dictum at tempor commodo ullamcorper. Tempor orci eu lobortis elementum nibh tellus molestie nunc. Nunc sed velit dignissim sodales ut eu sem integer vitae. Morbi tincidunt ornare massa eget egestas purus viverra. Nec ultrices dui sapien eget mi proin. Viverra mauris in aliquam sem fringilla ut morbi tincidunt. Volutpat ac tincidunt vitae semper. Ultricies mi eget mauris pharetra et ultrices. Bibendum at varius vel pharetra vel turpis nunc eget. Duis convallis convallis tellus id interdum velit laoreet. Ultricies integer quis auctor elit sed vulputate.";

        for (int i = 0; i < n - 1; i++)
        {
            builder.Append(s + "\n\n");
        }
        builder.Append(s);

        string body = builder.ToString();

        for (int i = 0; i < n; i++)
        {
            Random random = new Random();
            TimeSpan ts = TimeSpan.FromDays(random.Next(0, 100));

            Guid id = Guid.NewGuid();
            DateTimeOffset postDate = DateTimeOffset.Now.Subtract(ts);

            Article article = new()
            {
                Id = id,
                PostDate = postDate,
                Title = "Post Number " + id.ToString(),
                SubTitle = "A randomly generated post!",
                Body = body
            };

            repository.Add(article);
        }
    }
}