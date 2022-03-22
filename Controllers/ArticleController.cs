using BlogRest.Dtos;
using BlogRest.Repositories;
using BlogRest.Services;
using Microsoft.AspNetCore.Mvc;
namespace BlogRest.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService articleService;
    // private static readonly HttpClient httpClient = new HttpClient();

    public ArticleController(IArticleService articleService)
    {
        this.articleService = articleService;
    }

    [HttpGet]
    public IEnumerable<ArticleDto> GetAllArticles()
    {
        IEnumerable<ArticleDto> articles = articleService.GetAllArticles();
        return articles;
    }
    
    [HttpPost]
    public void PostNewArticle(string title, string subtitle, string body)
    {
        //TODO: This method does nothing atm. Need to introduce some kind of validation system.
        articleService.CreateNewArticle(title, subtitle, body);
    }
}