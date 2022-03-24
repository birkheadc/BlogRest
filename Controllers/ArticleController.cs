using BlogRest.Dtos;
using BlogRest.Models;
using BlogRest.Repositories;
using BlogRest.Services;
using Microsoft.AspNetCore.Mvc;
namespace BlogRest.Controllers;

[ApiController]
[Route("articles")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService articleService;
    // private static readonly HttpClient httpClient = new HttpClient();

    public ArticleController(IArticleService articleService)
    {
        this.articleService = articleService;
    }

    [HttpGet]
    public IActionResult GetAllArticles()
    {
        IEnumerable<ArticleDto> articles = articleService.GetAllArticles();
        return Ok(articles);
    }

    [HttpGet]
    [Route("title/{title}")]
    public IActionResult GetArticleByTitle(string title)
    {
        ArticleDto article = articleService.GetArticleByTitle(title);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article);
    }

    [HttpGet]
    [Route("profiles")]
    public IActionResult GetAllArticleProfiles()
    {
        IEnumerable<ArticleProfileDto> profiles = articleService.GetAllArticleProfiles();
        return Ok(profiles);
    }
    
    [HttpPost]
    public IActionResult PostNewArticle(string title, string subtitle, string body)
    {
        //TODO: This method does nothing atm. Need to introduce some kind of validation system.
        articleService.CreateNewArticle(title, subtitle, body);
        return StatusCode(403);
    }
}