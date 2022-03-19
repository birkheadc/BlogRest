using System.Collections;
using BlogRest.Entities;
using BlogRest.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogRest.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService articleService;

    public ArticleController(IArticleService articleService)
    {
        this.articleService = articleService;
    }

    [HttpGet]
    public IEnumerable<Article> GetAllArticles()
    {
        IEnumerable<Article> articles = articleService.GetAllArticles();
        return articles;
    }
}