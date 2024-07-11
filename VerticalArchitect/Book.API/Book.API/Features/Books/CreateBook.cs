using Book.API.Data;
using Carter;
using MediatR;

namespace Book.API.Features.Books
{
    //public static class CreateBook
    //{
    //    public static void AddEndpoint(this IEndpointRouteBuilder app) 
    //    {
    //        app.MapPost("/api/books", async (CreateBookCommand request, ISender sender) =>
    //        {
    //            var bookId = await sender.Send(request);
    //            return Results.Ok(bookId);
    //        });
    //    }
    //}

    public record CreateBookCommand(API.Entities.Book Book):IRequest<int>;

    internal class Handler(AppDbContext appDbContext) : IRequestHandler<CreateBookCommand, int>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            _appDbContext.Books.Add(request.Book);
            _appDbContext.SaveChanges();
            return Task.FromResult(request.Book.ID);
        }
    }

    public class CreateBookEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/books", async (CreateBookCommand request, ISender sender) =>
            {
                var bookId = await sender.Send(request);
                return Results.Ok(bookId);
            });
        }
    }
}
