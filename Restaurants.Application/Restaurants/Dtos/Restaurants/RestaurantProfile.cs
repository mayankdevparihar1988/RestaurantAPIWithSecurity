using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos.Dishes;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos.Restaurants;

public class RestaurantProfile: Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.Address, opt => opt.MapFrom(s => new Address()
            {
                City = s.City,
                PostalCode = s.PostalCode,
                Street = s.Street
            }))
            .ForMember(dest => dest.Dishes, opt => opt.Ignore());
      
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.City, o => o.MapFrom(s => s.Address == null ? null : s.Address.City))
            .ForMember(d => d.PostalCode, o => o.MapFrom(s => s.Address == null ? null : s.Address.PostalCode))
            .ForMember(d => d.Street, o => o.MapFrom(s => s.Address == null ? null : s.Address.Street))
            .ForMember(d => d.Dishes, o => o.MapFrom(s => s.Dishes));
        
        CreateMap<UpdateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.Address, opt => opt.MapFrom(s => new Address()
            {
                City = s.City,
                PostalCode = s.PostalCode,
                Street = s.Street
            }))
            .ForMember(dest => dest.Dishes, opt => opt.Ignore());
    }
    
}