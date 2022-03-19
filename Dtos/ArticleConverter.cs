using BlogRest.Models;

namespace BlogRest.Dtos;

public class ArticleConverter
{
    public Article DtoToEntity(ArticleDto dto)
    {
        Article entity = new()
        {
            Id = dto.Id,
            PostDate = dto.PostDate,
            Title = dto.Title,
            SubTitle = dto.SubTitle,
            Body = dto.Body
        };
        return entity;
    }

    public ArticleDto EntityToDto(Article entity)
    {
        ArticleDto dto = new()
        {
            Id = entity.Id,
            PostDate = entity.PostDate,
            Title = entity.Title,
            SubTitle = entity.SubTitle,
            Body = entity.Body
        };
        return dto;
    }

    public IEnumerable<Article> DtoToEntityIEnum(IEnumerable<ArticleDto> dtos)
    {
        List<Article> entities = new();

        foreach (ArticleDto dto in dtos)
        {
            Article entity = DtoToEntity(dto);
            entities.Add(entity);
        }

        return entities;
    }

    public IEnumerable<ArticleDto> EntityToDtoIEnum(IEnumerable<Article> entities)
    {
        List<ArticleDto> dtos = new();

        foreach (Article entity in entities)
        {
            ArticleDto dto = EntityToDto(entity);
            dtos.Add(dto);
        }

        return dtos;
    }
}