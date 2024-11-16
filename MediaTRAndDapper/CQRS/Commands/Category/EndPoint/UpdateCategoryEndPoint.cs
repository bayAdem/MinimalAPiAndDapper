using FastEndpoints;
using MediatR;
using MediaTRAndDapper.CQRS.Commands.Category.UpdateCategories;

namespace MediaTRAndDapper.CQRS.Commands.Category.EndPoint;


public class UpdateCategoryEndPoint(ISender sender) : Endpoint<UpdateCategoryRequest>
{

    public override void Configure()
    {
        Put("/UpdateCategory");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateCategoryRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);
        var command = new UpdateCategoryCommand(req.Id,
            req.Name,
            req.Description,
            req.CreatedAt,
            req.ProductIds);

        var response = sender.Send(command);
        if (response != null)
        {
            await SendAsync(new { Message = "Category updated successfully" }, StatusCodes.Status200OK, ct);
        }
        else
        {
            await SendAsync(new { Message = "Failed to updated successfully" }, StatusCodes.Status400BadRequest, ct);
        }
    }
}