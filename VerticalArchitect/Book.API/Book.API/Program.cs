using Book.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using Book.API.Features.Books;
using Carter;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt => 
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddCarter();
var app = builder.Build();

//CreateBook.AddEndpoint(app);
//GetAllBooks.AddEndpoint(app);


app.MapCarter();
app.UseHttpsRedirection();

app.Run();

