
namespace UserService.Features.RegisterCustomer;

public record RegisterCustomerRequest(string Email, string Password, string FullName);
public record RegisterCustomerResponse(Guid UserId);


public class RegisterCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users/register/customer", async (
                RegisterCustomerRequest request,
                ISender sender) =>
            {
                var command = request.Adapt<RegisterCustomerCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<RegisterCustomerResponse>();
                return Results.Ok(response);
            })
            .WithName("RegisterCustomer")
            .WithSummary("Register a new Customer")
            .WithDescription("Creates a new user account with role Customer")
            .Produces<RegisterCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);    
    }
}