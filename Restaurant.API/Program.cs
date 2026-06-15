using AutoMapper;
using Microsoft.OpenApi.Models;
using Restaurant.API.Middlewares;
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
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { 
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                    },
                    []
                }

            });
        });

        
        
        // Register Error Handling Middelware
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
        
        // Adding Database context dependancy
        builder.Services.AddRestaurantDbContext(builder.Configuration);
        builder.Services.AddApplication();
        
        // Using Serilogs
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration)
        );


        var app = builder.Build();
        
        
        // As IRestaurantSeeder is an scoped service so we get it from dependency injection and run it
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
        app.MapGroup("api/identity").MapIdentityApi<User>();
        
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