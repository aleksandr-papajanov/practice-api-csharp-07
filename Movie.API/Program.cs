using Microsoft.EntityFrameworkCore;
using Movie.API.Helpers;
using Movie.API.Middleware;
using Movie.Contracts.Services;
using Movie.Core;
using Movie.Core.Abstractions;
using Movie.Core.Abstractions.Repositories;
using Movie.Data;
using Movie.Data.Repositories;
using Movie.Presentation;
using Movie.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")
        ?? throw new InvalidOperationException("Connection string 'MovieContext' not found.")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IFilmActorRepository, FilmActorRepository>();
builder.Services.AddScoped<IFilmDetailsRepository, FilmDetailsRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddScoped(provider => new Lazy<IFilmRepository>(() => provider.GetRequiredService<IFilmRepository>()));
builder.Services.AddScoped(provider => new Lazy<IActorRepository>(() => provider.GetRequiredService<IActorRepository>()));
builder.Services.AddScoped(provider => new Lazy<IFilmActorRepository>(() => provider.GetRequiredService<IFilmActorRepository>()));
builder.Services.AddScoped(provider => new Lazy<IFilmDetailsRepository>(() => provider.GetRequiredService<IFilmDetailsRepository>()));
builder.Services.AddScoped(provider => new Lazy<IReviewRepository>(() => provider.GetRequiredService<IReviewRepository>()));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddControllers(setup =>
{
    // Add custom middleware for model validation
    setup.Filters.Add<ValidateModelStateFilterAttribute>();
})
    .ConfigureApiBehaviorOptions(setup =>
    {
        // For custom model validation to work
        setup.SuppressModelStateInvalidFilter = true;
    })
    .AddApplicationPart(typeof(PresentationAssemblyReference).Assembly);

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var path = Path.Combine(AppContext.BaseDirectory, $"{typeof(PresentationAssemblyReference).Assembly.GetName().Name}.xml");
    o.IncludeXmlComments(path);

    path = Path.Combine(AppContext.BaseDirectory, $"{typeof(CoreAssemblyReference).Assembly.GetName().Name}.xml");
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
