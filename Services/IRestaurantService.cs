using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int CreateRestaurant(CreateRestaurantDto createRestaurantDto);
        IEnumerable<RestaurantDto> GetAllRestaurants();
        RestaurantDto GetRestaurantById(int id);
        bool DeleteRestaurant(int id);
        bool UpdateRestaurant(int id, UpdateRestaurantDto dto);
    }
}