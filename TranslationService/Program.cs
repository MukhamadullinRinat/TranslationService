using MediatR;
using System.Reflection;
using TranslationService.Application.Book.V1;
using TranslationService.Domain;
using TranslationService.Domain.Book.V1;
using TranslationService.Domain.Book.V1.POST;
using TranslationService.Infrastructure.Repositories;
using BookDTO = TranslationService.Domain.Book.V1.Book;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRequestHandler<BookRequest, BookDTO>, BookPostHandler>();
builder.Services.AddScoped<IRepository<Book>, BookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
