using Bit.Application.Articles.Commands.CreateArticle;
using Bit.Application.Articles.Commands.DeleteArticle;
using Bit.Application.Articles.Commands.UpdateArticle;
using Bit.Application.Articles.Queries.GetArticles;
using Bit.Application.Articles.Queries.GetArticlesWithPagination;
using Bit.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bit.Web.Endpoints;

public class Articles : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
          //.RequireAuthorization()
          .MapGet(GetArticlesWithPagination, "WithPagination")
          .MapGet(GetArticles)
          .MapPost(CreateArticle)
          .MapPut(UpdateArticle, "{id}")
          //.MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
          .MapDelete(DeleteArticle, "{id}");

    }

    public async Task<Ok<PaginatedList<ArticleBriefDto>>> GetArticlesWithPagination(ISender sender, [AsParameters] GetArticlesWithPaginationQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    public async Task<Ok<IEnumerable<ArticlesVm>>> GetArticles(ISender sender, [AsParameters] GetArticlesQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateArticle(ISender sender, CreateArticleCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(Articles)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateArticle(ISender sender, int id, UpdateArticleCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    //public async Task<Results<NoContent, BadRequest>> UpdateTodoItemDetail(ISender sender, int id, UpdateTodoItemDetailCommand command)
    //{
    //    if (id != command.Id) return TypedResults.BadRequest();

    //    await sender.Send(command);

    //    return TypedResults.NoContent();
    //}

    public async Task<NoContent> DeleteArticle(ISender sender, int id)
    {
        await sender.Send(new DeleteArticleCommand(id));

        return TypedResults.NoContent();
    }
}
