using Bit.Domain.Entities;

namespace Bit.Application.Articles.Queries.GetArticles;
public class ArticlesVm
{
    public string? Title { get; init; }
    public string? Content { get; init; }
    public string? Description { get; init; }
    public string? Url { get; init; }
    public string? ImageUrl { get; init; }
    public string? Category { get; init; }
    //public string[]? Category { get; init; }
    public string? Tags { get; init; }
    public bool? IsPublished { get; init; }
    public int? Rating { get; init; }
    public DateTime? PublishedDate { get; init; }
    public int? Views { get; init; }
    public int? Likes { get; init; }
    public string? Comments { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Article, ArticlesVm>();
            //CreateMap<Article, ArticlesVm>().ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category.Split(",").ToList()));
        }
    }
}
