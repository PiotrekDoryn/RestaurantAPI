using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(RestaurantDBcontext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
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

        public bool DeleteRestaurant(int id)
        {
            _logger.LogWarning($"Restaurant with Id {id} Delete action invoked.");

            var result = _dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (result is null)
               return false;

            _dbContext.Restaurants.Remove(result);
            _dbContext.SaveChanges();

            return true;
        }

        public bool UpdateRestaurant(int id, UpdateRestaurantDto dto)
        {
            var result = _dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (result is null)
                return false;

            result.Name = dto.Name;
            result.Description = dto.Description;
            result.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();

            return true;
        }
    }
}