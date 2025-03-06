using Bit.Application.Common.Interfaces;
using Bit.Application.Common.Mappings;
using Bit.Application.Common.Models;

namespace Bit.Application.Articles.Queries.GetArticlesWithPagination;

public record GetArticlesWithPaginationQuery : IRequest<PaginatedList<ArticleBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetArticlesWithPaginationQueryValidator : AbstractValidator<GetArticlesWithPaginationQuery>
{
    public GetArticlesWithPaginationQueryValidator()
    {
    }
}

public class GetArticlesWithPaginationQueryHandler : IRequestHandler<GetArticlesWithPaginationQuery, PaginatedList<ArticleBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArticlesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ArticleBriefDto>> Handle(GetArticlesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Articles
            .OrderBy(x => x.Id)
            .ProjectTo<ArticleBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
           //.Where(x => x.ListId == request.ListId)
           //.OrderBy(x => x.Title)
           //.ProjectTo<TodoItemBriefDto>(_mapper.ConfigurationProvider)
           //.PaginatedListAsync(request.PageNumber, request.PageSize);

        //return await Task.FromResult(new PaginatedList<ArticleBriefDto>());
    }
}
