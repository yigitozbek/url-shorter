using System.ComponentModel.DataAnnotations;
using API.Context;
using API.Entities;
using API.Helpers;
using API.Options;
using API.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseInMemoryDatabase("LocalDb");
});

builder.Services.AddHttpContextAccessor();


var shortenerOption = builder.Configuration.GetSection("Shortener").Get<ShortenerOption>();

builder.Services.AddSingleton(shortenerOption);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/link/add", ([FromBody] string url, [FromServices] ApplicationDbContext context ) =>
{

    if (string.IsNullOrEmpty(url))
    {
        throw new ValidationException("Url cannot be empty");
    }

    if (!UrlValidator.IsValidUrl(url))
    {
        throw new ValidationException("Invalid URL");
    }
    
    var options = app.Services.GetRequiredService<ShortenerOption>();
    
    var code = CodeGenerator.GenerateCode(options.ShortUrlLength,options.ShortUrlCharacters);
    
    // TODO : check if code is unique
    
    var shortUrl = $"{options.BaseUrl}/{code}";
    var expDate = DateTimeOffset.Now.AddMinutes(options.ShortUrlExpiration);

    var entity = new Url
    {
        ShortUrl = shortUrl,
        OriginalUrl = url,
        Expiration = expDate,
        Created = DateTimeOffset.Now
    };

    context.Url.Add(entity);

    context.SaveChanges();

    return Results.Ok(new
    {
        shortUrl,
        expDate
    });
});

app.MapGet("link/{code}/analyze", (string code, [FromServices] ApplicationDbContext context) =>
{
    var options = app.Services.GetRequiredService<ShortenerOption>();

    var shortUrl = $"{options.BaseUrl}/{code}";

    var entity = context.Url
        .AsQueryable()
        .AsNoTracking()
        .Include(x => x.Analytics)
        .FirstOrDefault(x => x.ShortUrl == shortUrl)
        ;

    if (entity is null)
    {
        return Results.NotFound("Link not found");
    }

    return Results.Ok(new
    {
        entity.OriginalUrl,
        entity.Expiration,
        entity.Created,
        Analytics = entity.Analytics?.Select(x => new
        {
            x.UserAgent,
            x.IpAddress,
            x.Referer,
            x.Created
        })
    });

});


app.MapGet("{code}", (string code, [FromServices] ApplicationDbContext context) =>
{
    var options = app.Services.GetRequiredService<ShortenerOption>();
    
    var shortUrl = $"{options.BaseUrl}/{code}";
    

    var entity = context.Url
        .AsQueryable()
        .AsNoTracking()
        .FirstOrDefault(x => x.ShortUrl == shortUrl);

    if (entity is null)
    {
        return Results.NotFound("Link not found");
    }
    
    var contextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();

    var analytic = new Analytic
    {
        UrlId = entity.Id,
        UserAgent = contextAccessor?.HttpContext?.Request.Headers.UserAgent,
        IpAddress = contextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString(),
        Referer = contextAccessor?.HttpContext?.Request.Headers.Referer,
        Created = DateTimeOffset.Now
    };
    
    context.Analytic.Add(analytic);
    
    context.SaveChanges();
    return Results.Redirect(entity.OriginalUrl);
});

app.Run();  