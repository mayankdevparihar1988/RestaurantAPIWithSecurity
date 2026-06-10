using AutoMapper;
using AutoMapper.Internal;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos.Restaurants;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(CreateRestaurantCommand).Assembly;
        services.AddScoped<IRestaurantServices, RestaurantServices>();
        // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAutoMapper(cfg => 
        {
            // 1. License is REQUIRED in v16 (Community/Free key is available)
            cfg.LicenseKey = "COMMUNITY-DISK-FREE-2026"; 

            // 2. Setting MaxDepth globally in v16
            cfg.Internal().ForAllMaps((typeMap, mappingExpression) => 
            {
                mappingExpression.MaxDepth(32);
            });
         

        }, assembly);
        // 1. Register all validators from the assembly where your DTOs/Validators live
        services.AddValidatorsFromAssembly(assembly);
        // 2. Enable automatic validation for Controllers
        services.AddFluentValidationAutoValidation();
        // Registering the mediator 
        services.AddMediatR(cnf => cnf.RegisterServicesFromAssembly(assembly));

    }
}