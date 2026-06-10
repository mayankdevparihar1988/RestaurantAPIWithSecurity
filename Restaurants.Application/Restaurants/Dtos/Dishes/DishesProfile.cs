using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos.Dishes;

public class DishesProfile: Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDto>()
            .ForMember(dest => dest.RestaurantId, opt => opt.Ignore());
        CreateMap<DishDto, Dish>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Restaurant, opt => opt.Ignore());

    }
}