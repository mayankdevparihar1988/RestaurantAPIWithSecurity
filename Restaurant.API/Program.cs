using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;

namespace Restaurant.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // Adding Database context dependancy
        builder.Services.AddRestaurantDbContext(builder.Configuration);
        builder.Services.AddApplication();

        var app = builder.Build();
        
        
        // As IRestaurantSeeder is an scoped service so we get it from dependency injection and run it
        var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

        await seeder.Seed();

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
    }
}