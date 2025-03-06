using Bit.Application.Common.Interfaces;

namespace Bit.Application.Articles.Queries.GetArticles;

public record GetArticlesQuery : IRequest<IEnumerable<ArticlesVm>>
{
}

public class GetArticlesQueryValidator : AbstractValidator<GetArticlesQuery>
{
    public GetArticlesQueryValidator()
    {
    }
}

public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, IEnumerable<ArticlesVm>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArticlesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ArticlesVm>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Articles
            .ProjectTo<ArticlesVm>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.Title)
            .ToListAsync();
    }
}
