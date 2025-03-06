using Bit.Domain.Entities;

namespace Bit.Application.Articles.Queries.GetArticlesWithPagination;
public class ArticleBriefDto
{
    public string? Title { get; init; }

    public string? Content { get; init; }

    public string? Description { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Article, ArticleBriefDto>();
        }
    }
}
