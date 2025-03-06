namespace Bit.Domain.Entities;
public class Article : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? ImageUrl { get; set; }
    public string? Category { get; set; }
    public string? Tags { get; set; }
    public bool? IsPublished { get; set; }
    public int? Rating { get; set; }
    public DateTime? PublishedDate { get; set; }
    public int? Views { get; set; }
    public int? Likes { get; set; }
    public string? Comments { get; set; }
}
