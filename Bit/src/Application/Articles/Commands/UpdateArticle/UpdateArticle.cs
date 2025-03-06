using Bit.Application.Common.Interfaces;

namespace Bit.Application.Articles.Commands.UpdateArticle;

public record UpdateArticleCommand : IRequest
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
    public string? Description { get; init; }
    public string? Url { get; init; }
    public string? ImageUrl { get; init; }
    public string? Category { get; init; }
    public string? Tags { get; init; }
    public bool? IsPublished { get; init; }
    public int? Rating { get; init; }
    public DateTime? PublishedDate { get; init; }
    public int? Views { get; init; }
    public int? Likes { get; init; }
    public string? Comments { get; init; }
}

public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateArticleCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(UpdateArticleCommand model, string title, CancellationToken cancellationToken)
    {
        return !await _context.TodoLists
            .Where(l => l.Id != model.Id)
            .AnyAsync(l => l.Title == title, cancellationToken);
    }

}

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateArticleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Articles
           .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        entity.Content = request.Content;
        entity.Description = request.Description;
        entity.Url = request.Url;
        entity.ImageUrl = request.ImageUrl;
        entity.Category = request.Category;
        entity.Tags = request.Tags;
        entity.IsPublished = request.IsPublished;
        entity.Rating = request.Rating;
        entity.PublishedDate = request.PublishedDate;
        entity.Views = request.Views;
        entity.Likes = request.Likes;
        entity.Comments = request.Comments;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
