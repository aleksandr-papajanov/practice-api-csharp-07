using Movie.API.Extentions;
using Movie.API.Middleware;
using Movie.Core;
using Movie.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddHostedServices();

builder.Services.AddControllers(setup =>
{
    // Add custom middleware for model validation
    setup.Filters.Add<ModelStateValidationFilterAttribute>();
})
    .AddNewtonsoftJson()
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
    app.UseSwagger();
    app.UseSwaggerUI(); // можно передать опции, например endpoint, заголовок и т.д.
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
