using MediatR;
using Microsoft.AspNetCore.Authentication;
using System.Reflection;
using TranslationService.Application.Book.V1;
using TranslationService.Application.User.V1.Auth;
using TranslationService.Application.Word.V1;
using TranslationService.Domain;
using TranslationService.Domain.Book.V1.DELETE;
using TranslationService.Domain.Book.V1.File;
using TranslationService.Domain.Book.V1.GET;
using TranslationService.Domain.Book.V1.List;
using TranslationService.Domain.Book.V1.POST;
using TranslationService.Domain.Book.V1.PUT;
using TranslationService.Domain.User.V1.Auth;
using TranslationService.Domain.Word.V1.DELETE;
using TranslationService.Domain.Word.V1.GET;
using TranslationService.Domain.Word.V1.List;
using TranslationService.Domain.Word.V1.POST;
using TranslationService.Domain.Word.V1.PUT;
using TranslationService.Infrastructure.Repositories;
using BookEntity = TranslationService.Domain.Book.V1.Book;
using WordEntity = TranslationService.Domain.Word.V1.Word;
using UserEntity = TranslationService.Domain.User.User;
using TranslationService.Domain.User.V1.List;
using TranslationService.Application.User.V1;
using TranslationService.Domain.User.V1.POST;
using TranslationService.Domain.User.V1.GET;
using TranslationService.Domain.User.V1.PUT;
using TranslationService.Domain.User.V1.DELETE;
using Microsoft.OpenApi.Models;
using TranslationService.Application.User;
using TranslationService.Domain.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasicAuth", Version = "v1" });
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "basic"
                    }
                },
                new string[] {}
        }
    });
});

builder.Services.AddScoped<IRequestHandler<BookRequestPost, BookEntity>, BookPostHandler>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IRequestHandler<BookRequestGet, BookEntity>, BookGetHandler>();
builder.Services.AddScoped<IRequestHandler<BookFilter, IEnumerable<BookEntity>>, BookListHandler>();
builder.Services.AddScoped<IRequestHandler<BookRequestPut, BookEntity>, BookPutHandler>();
builder.Services.AddScoped<IRequestHandler<BookRequestDelete, Guid>, BookDeleteHandler>();
builder.Services.AddScoped<IRequestHandler<BookRequestFileGet, BookFileResponse>, BookFileGetHandler>();

builder.Services.AddScoped<IRepository<WordEntity, WordEntity, WordFilter>, WordRepository>();
builder.Services.AddScoped<IRequestHandler<WordRequestGet, WordEntity>, WordGetHandler>();
builder.Services.AddScoped<IRequestHandler<WordRequestPost, WordEntity>, WordPostHandler>();
builder.Services.AddScoped<IRequestHandler<WordFilter, IEnumerable<WordEntity>>, WordListHandler>();
builder.Services.AddScoped<IRequestHandler<WordRequestPut, WordEntity>, WordPutHandler>();
builder.Services.AddScoped<IRequestHandler<WordRequestDelete, Guid>, WordDeleteHandler>();

builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<IRepository<UserEntity, UserEntity, UserFilter>, UserRepository>();
builder.Services.AddScoped<IRequestHandler<UserPostRequest, UserEntity>, UserPostHandler>();
builder.Services.AddScoped<IRequestHandler<UserGetRequest, UserEntity>, UserGetHandler>();
builder.Services.AddScoped<IRequestHandler<UserPutRequest, UserEntity>, UserPutHandler>();
builder.Services.AddScoped<IRequestHandler<UserFilter, IEnumerable<UserEntity>>, UserListHandler>();
builder.Services.AddScoped<IRequestHandler<UserDeleteRequest, Guid>, UserDeleteHandler>();
builder.Services.AddScoped<IRequestHandler<UserAuthRequest, UserEntity>, UserAuthHandler>();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
