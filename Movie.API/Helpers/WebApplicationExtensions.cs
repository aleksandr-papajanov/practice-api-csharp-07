using Movie.Data;

namespace Movie.API.Helpers
{
    internal static class WebApplicationExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<AppDbContext>();

                var seeder = new DataSeeder(context);
                await seeder.SeedAsync();
            }
        }
    }
}
