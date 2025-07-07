using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PracticeApiCSharp07.Helpers;
using PracticeApiCSharp07.Infrastructure;
using PracticeApiCSharp07.Middleware;
using PracticeApiCSharp07.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")
        ?? throw new InvalidOperationException("Connection string 'MovieContext' not found.")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var path = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    o.IncludeXmlComments(path);
});

var app = builder.Build();

// Turn on Swagger only in development mode
if (app.Environment.IsDevelopment())
{
    await app.SeedDataAsync();
    app.UseSwagger();
    app.UseSwaggerUI(); // можно передать опции, например endpoint, заголовок и т.д.
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
