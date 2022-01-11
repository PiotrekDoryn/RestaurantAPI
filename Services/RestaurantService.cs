using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDBcontext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantService(RestaurantDBcontext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RestaurantDto> GetAllRestaurants()
        {
            var result = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var resultDto = _mapper.Map<List<RestaurantDto>>(result);
            return resultDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RestaurantDto GetRestaurantById(int id)
        {
            var result = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == id);

            if (result is null)            
                return null;
            
            var resultDto = _mapper.Map<RestaurantDto>(result);
            return resultDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createRestaurantDto"></param>
        public int CreateRestaurant(CreateRestaurantDto createRestaurantDto)
        {
            var result = _mapper.Map<Restaurant>(createRestaurantDto);

            _dbContext.Restaurants.Add(result);
            _dbContext.SaveChanges();

            return result.Id;
        }
    }
}