using FastEndpoints;
using MediatR;
using MediaTRAndDapper.CQRS.Commands.Category.DeleteCategories;

namespace MediaTRAndDapper.CQRS.Commands.Category.EndPoint;


public class DeleteCateogryEndPoint(ISender sender) : Endpoint<DeleteCategoryRequest>
{
    public override void Configure()
    {
        Delete("/DeleteCategory");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteCategoryRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        var command = new DeleteCategoryCommand(req.Id);
        var response = sender.Send(command);
        if (response != null)
        {
            await SendAsync(new { Message = "Category deleted successfully" }, StatusCodes.Status200OK, ct);
        }
        else
        {
            await SendAsync(new { Message = "Failed to delete category" }, StatusCodes.Status400BadRequest, ct);
        }
    }
}
