using Book.API.Data;
using Carter;
using FluentValidation;
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

    public record CreateBookCommand(API.Entities.Book Book) : IRequest<int>;

    public class Validator : AbstractValidator<CreateBookCommand>
    {
        public Validator()
        {

            RuleFor(B => B.Book.Name)
                .NotEmpty().WithMessage("Name cannot be empty.");

            RuleFor(x => x.Book.Title)
                .NotEmpty().WithMessage("Title cannot be empty.");

            RuleFor(x => x.Book.Description)
                .NotEmpty().WithMessage("Description cannot be empty.");

            RuleFor(x => x.Book.AuthorName)
                .NotEmpty().WithMessage("Author name cannot be empty.");

        }

    }
    internal class Handler(AppDbContext appDbContext,IValidator<CreateBookCommand> validator) : IRequestHandler<CreateBookCommand, int>
    {
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly IValidator<CreateBookCommand> _validator=validator;

        public Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            // Validate the request using FluentValidation
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors;

                throw new ValidationException("Validation failed", errors);
            }
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
