using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Mapper
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(x => x.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(x => x.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(x => x.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>().
                ForMember(x => x.Address, c => c.MapFrom(dto => new Address() { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));
        }
    }
}
