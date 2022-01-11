using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private RestaurantDBcontext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantController(RestaurantDBcontext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Return All Restaurants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(RestaurantDto), 200)]
        public ActionResult<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            var result = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var resultDto = _mapper.Map<List<RestaurantDto>>(result);
            return Ok(resultDto);
        }

        /// <summary>
        /// Return All Restaurant by Id number
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RestaurantDto>> GetRestaurantById([FromRoute] int id)
        {
            var result = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == id);

            if(result is null)
            {
                return NotFound();
            }
            var resultDto = _mapper.Map<RestaurantDto>(result);
            return Ok(resultDto);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createRestaurantDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            var result = _mapper.Map<Restaurant>(createRestaurantDto);
            _dbContext.Restaurants.Add(result);
            _dbContext.SaveChanges();

            return Created($"/api/restaurant/{result.Id}",null);      
        }
    }
}
