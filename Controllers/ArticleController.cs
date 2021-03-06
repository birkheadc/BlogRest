using System.Net;
using BlogRest.Dtos;
using BlogRest.Exceptions;
using BlogRest.Filters;
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
        try
        {
            ArticleDto article = articleService.GetArticleByTitle(title);
            return Ok(article);
        }
        catch(FileNotFoundException e)
        {
            return NotFound();
        }
        
    }

    [HttpGet]
    [Route("profiles")]
    public IActionResult GetAllArticleProfiles()
    {
        IEnumerable<ArticleProfileDto> profiles = articleService.GetAllArticleProfiles();
        return Ok(profiles);
    }

    [HttpGet]
    [Route("profiles/recent/{n}")]
    public IActionResult GetNRecentArticleProfiles(int n)
    {
        IEnumerable<ArticleProfileDto> profiles = articleService.GetNRecentArticleProfiles(n);
        return Ok(profiles);
    }
    
    [HttpPost]
    [ApiKeyAuth]
    public IActionResult PostNewArticle([FromBody] InboundArticleDto inboundArticleDto)
    {
        try
        {
            articleService.CreateNewArticle(inboundArticleDto);
        }
        catch(ArticleTitleExistsException e)
        {
            return Conflict();
        }
        catch(ArgumentException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Article published successfully!");
    }

    [HttpPost]
    [Route("test")]
    [ApiKeyAuth]
    public IActionResult AddTestArticles()
    {
        articleService.CreateTestArticles();
        return Ok();
    }

    [HttpPost]
    [Route("debug")]
    public IActionResult Debug([FromBody] InboundArticleDto inboundArticleDto)
    {
        return Ok(inboundArticleDto);
    }
}