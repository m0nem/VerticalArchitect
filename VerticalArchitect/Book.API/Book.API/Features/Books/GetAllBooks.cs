﻿using Book.API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Features.Books
{
    public static class GetAllBooks
    {
        public static void AddEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/books", async (ISender sender) =>
            {
                var books = await sender.Send(new Query());
                return Results.Ok(books);
            });
        }
    }

    public record Query:IRequest<List<API.Entities.Book>>;

    internal class QueryHandler : IRequestHandler<Query, List<API.Entities.Book>>
    {
        private readonly AppDbContext _appDbContext;
        public QueryHandler(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }
        public Task<List<Entities.Book>> Handle(Query request, CancellationToken cancellationToken)
        {
            var books=_appDbContext.books.AsNoTracking().ToList();
            return Task.FromResult(books);
        }
    }
}