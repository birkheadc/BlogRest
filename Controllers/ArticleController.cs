using BlogRest.Dtos;
using BlogRest.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace BlogRest.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticleController : ControllerBase
{
    private readonly IArticleRepository repository;
    // private static readonly HttpClient httpClient = new HttpClient();

    public ArticleController(IArticleRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public IEnumerable<ArticleDto> GetAllArticles()
    {
        IEnumerable<ArticleDto> articles = repository.GetAllArticles();
        return articles;
    }
    
    [HttpPost]
    public void PostNewArticle(string title, string subtitle, string body)
    {
        //TODO: Some kind of user validation
        repository.CreateNewArticle(title, subtitle, body);
    }
}