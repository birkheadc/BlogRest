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
        //TODO: Use some kind of validation.
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

    public void CreateTestArticles()
    {
        int n = 10;

        for (int i = 0; i < n; i++) {

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
                Body = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nunc mattis enim ut tellus elementum sagittis vitae et leo. Euismod nisi porta lorem mollis aliquam ut porttitor leo a. Tempus imperdiet nulla malesuada pellentesque elit. Odio tempor orci dapibus ultrices in iaculis. Tellus mauris a diam maecenas sed. Eu volutpat odio facilisis mauris sit amet massa vitae tortor. Id cursus metus aliquam eleifend mi in nulla posuere sollicitudin. Varius duis at consectetur lorem donec massa sapien faucibus et. Risus in hendrerit gravida rutrum. Lorem ipsum dolor sit amet consectetur. Semper quis lectus nulla at volutpat diam ut. Lobortis scelerisque fermentum dui faucibus in ornare. Magna fermentum iaculis eu non diam.

Convallis aenean et tortor at risus viverra. Orci porta non pulvinar neque. Felis bibendum ut tristique et egestas quis ipsum suspendisse ultrices. Felis eget nunc lobortis mattis aliquam faucibus purus in massa. Maecenas volutpat blandit aliquam etiam erat velit scelerisque in. Orci a scelerisque purus semper eget duis at. Elit scelerisque mauris pellentesque pulvinar pellentesque habitant morbi tristique. Pharetra vel turpis nunc eget. Vitae justo eget magna fermentum. Vivamus arcu felis bibendum ut tristique et egestas quis. Pharetra sit amet aliquam id. Et tortor at risus viverra adipiscing at in tellus integer. Diam quam nulla porttitor massa id neque aliquam vestibulum. Egestas tellus rutrum tellus pellentesque eu tincidunt tortor aliquam nulla. Sed libero enim sed faucibus turpis. Dolor magna eget est lorem ipsum dolor sit. Eleifend mi in nulla posuere sollicitudin.

Morbi tristique senectus et netus et malesuada fames. Diam vulputate ut pharetra sit. Enim nulla aliquet porttitor lacus luctus. Lorem donec massa sapien faucibus et molestie ac feugiat. Nec feugiat nisl pretium fusce id velit. Tristique senectus et netus et. Porta nibh venenatis cras sed felis eget velit aliquet sagittis. Purus faucibus ornare suspendisse sed nisi lacus sed. Auctor urna nunc id cursus metus aliquam eleifend. Ullamcorper eget nulla facilisi etiam dignissim diam quis.

Eu volutpat odio facilisis mauris sit. Sodales ut etiam sit amet nisl purus in mollis. Quis viverra nibh cras pulvinar mattis nunc sed blandit libero. Habitant morbi tristique senectus et netus. Et netus et malesuada fames ac turpis egestas integer. Volutpat sed cras ornare arcu dui vivamus. Convallis a cras semper auctor neque vitae. Mauris ultrices eros in cursus turpis massa. Quis ipsum suspendisse ultrices gravida dictum fusce ut. Netus et malesuada fames ac. Mauris commodo quis imperdiet massa tincidunt. Posuere lorem ipsum dolor sit amet consectetur. In hac habitasse platea dictumst vestibulum. Quisque egestas diam in arcu cursus euismod quis viverra nibh. Tellus in metus vulputate eu scelerisque felis.

Dictum sit amet justo donec enim diam vulputate ut. In aliquam sem fringilla ut morbi tincidunt augue interdum. Cum sociis natoque penatibus et magnis. Eget nunc scelerisque viverra mauris. Pulvinar neque laoreet suspendisse interdum consectetur libero id faucibus. Nam libero justo laoreet sit amet cursus sit. Lorem ipsum dolor sit amet consectetur adipiscing elit pellentesque. Lectus mauris ultrices eros in cursus turpis massa tincidunt. Blandit turpis cursus in hac habitasse platea. Egestas erat imperdiet sed euismod nisi porta lorem. In nisl nisi scelerisque eu ultrices. Habitant morbi tristique senectus et netus et malesuada fames ac. Fermentum et sollicitudin ac orci phasellus egestas tellus rutrum. Convallis aenean et tortor at. Nibh tortor id aliquet lectus proin nibh nisl condimentum. Justo donec enim diam vulputate ut pharetra sit amet. In tellus integer feugiat scelerisque. Urna neque viverra justo nec ultrices dui sapien. Cras adipiscing enim eu turpis egestas pretium."
            };
            repository.Add(article);
        }
    }
}