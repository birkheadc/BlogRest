using System.Collections.Generic;
using BlogRest.Entities;

namespace BlogRest.Services;

public interface IArticleService
{
    public IEnumerable<Article> GetAllArticles();
}