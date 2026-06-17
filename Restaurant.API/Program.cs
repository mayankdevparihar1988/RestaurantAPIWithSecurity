using AutoMapper;
using Microsoft.OpenApi.Models;
using Restaurant.API.Middlewares;
using Restaurants.API.Extensions;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;

namespace Restaurant.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        
        builder.Services.AddControllers();
        

        
      
        // Adding Database context dependency
        builder.AddPresentation();
        builder.Services.AddRestaurantDbContext(builder.Configuration);
        builder.Services.AddApplication();
        
        var app = builder.Build();
        
        
        // As IRestaurantSeeder is a scoped service so we get it from dependency injection and run it
        var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
        // Calling db seeding
        await seeder.Seed();
        
        // get automapper and validate if mapping configuration is missing
        var mapper =  scope.ServiceProvider.GetRequiredService<IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseAuthorization();

        app.MapControllers();
        
        // Enable the Identity api in the app, we do it after other controller
        app.MapGroup("api/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();
        
        // TEST USER
        // "email": "mayank.dev.parihar@gmail.com",
        // "password": "Mayank@123"
        
        // Configure the HTTP request pipeline.
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<RequestTimeLoggingMiddleware>();
        
        // Configure request loggin using serilogs
        app.UseSerilogRequestLogging();


        app.Run();
    }
}