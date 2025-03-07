using Bit.Application.Common.Interfaces;
using Bit.Domain.Entities;

namespace Bit.Application.Articles.Commands.CreateArticle;

public record CreateArticleCommand : IRequest<int>
{
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

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateArticleCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }
    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return !await _context.Articles
            .AnyAsync(l => l.Title == title, cancellationToken);
    }
}

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateArticleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Article
        {
            Title = request.Title,
            Content = request.Content,
            Description = request.Description,
            Url = request.Url,
            ImageUrl = request.ImageUrl,
            Category = request.Category,
            Tags = request.Tags,
            IsPublished = request.IsPublished,
            Rating = request.Rating,
            PublishedDate = request.PublishedDate,
            Views = request.Views,
            Likes = request.Likes,
            Comments = request.Comments

        };

        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        _context.Articles.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;

        //throw new NotImplementedException();
        //return await Task.FromResult(0);
    }
}
