using MediatR;
using System.Reflection;
using TranslationService.Application.Book.V1;
using TranslationService.Domain;
using TranslationService.Domain.Book.V1.DELETE;
using TranslationService.Domain.Book.V1.GET;
using TranslationService.Domain.Book.V1.List;
using TranslationService.Domain.Book.V1.POST;
using TranslationService.Domain.Book.V1.PUT;
using TranslationService.Infrastructure.Repositories;
using BookDTO = TranslationService.Domain.Book.V1.Book;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRequestHandler<BookRequestPost, BookDTO>, BookPostHandler>();
builder.Services.AddScoped<IRepository<BookDTO, BookFilter>, BookRepository>();
builder.Services.AddScoped<IRequestHandler<BookRequestGet, BookDTO>, BookGetHandler>();
builder.Services.AddScoped<IRequestHandler<BookFilter, IEnumerable<BookDTO>>, BookListHandler>();
builder.Services.AddScoped<IRequestHandler<BookRequestPut, BookDTO>, BookPutHandler>();
builder.Services.AddScoped<IRequestHandler<BookRequestDelete, Guid>, BookDeleteHandler>();

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
