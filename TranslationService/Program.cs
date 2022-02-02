using MediatR;
using System.Reflection;
using TranslationService.Application.Book.V1;
using TranslationService.Application.Word.V1;
using TranslationService.Domain;
using TranslationService.Domain.Book;
using TranslationService.Domain.Book.V1.DELETE;
using TranslationService.Domain.Book.V1.File;
using TranslationService.Domain.Book.V1.GET;
using TranslationService.Domain.Book.V1.List;
using TranslationService.Domain.Book.V1.POST;
using TranslationService.Domain.Book.V1.PUT;
using TranslationService.Domain.Word.V1.DELETE;
using TranslationService.Domain.Word.V1.GET;
using TranslationService.Domain.Word.V1.List;
using TranslationService.Domain.Word.V1.POST;
using TranslationService.Domain.Word.V1.PUT;
using TranslationService.Infrastructure.Repositories;
using BookDTO = TranslationService.Domain.Book.V1.Book;
using WordDTO = TranslationService.Domain.Word.V1.Word;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRequestHandler<BookRequestPost, BookDTO>, BookPostHandler>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IRequestHandler<BookRequestGet, BookDTO>, BookGetHandler>();
builder.Services.AddScoped<IRequestHandler<BookFilter, IEnumerable<BookDTO>>, BookListHandler>();
builder.Services.AddScoped<IRequestHandler<BookRequestPut, BookDTO>, BookPutHandler>();
builder.Services.AddScoped<IRequestHandler<BookRequestDelete, Guid>, BookDeleteHandler>();
builder.Services.AddScoped<IRequestHandler<BookRequestFileGet, BookFileResponse>, BookFileGetHandler>();

builder.Services.AddScoped<IRepository<WordDTO, WordDTO, WordFilter>, WordRepository>();
builder.Services.AddScoped<IRequestHandler<WordRequestGet, WordDTO>, WordGetHandler>();
builder.Services.AddScoped<IRequestHandler<WordRequestPost, WordDTO>, WordPostHandler>();
builder.Services.AddScoped<IRequestHandler<WordFilter, IEnumerable<WordDTO>>, WordListHandler>();
builder.Services.AddScoped<IRequestHandler<WordRequestPut, WordDTO>, WordPutHandler>();
builder.Services.AddScoped<IRequestHandler<WordRequestDelete, Guid>, WordDeleteHandler>();

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
