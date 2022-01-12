using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="restaurantService"></param>
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        /// <summary>
        /// Return All Restaurants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(RestaurantDto), 200)]
        public ActionResult<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            var resultDto = _restaurantService.GetAllRestaurants();
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
            var resultDto = _restaurantService.GetRestaurantById(id);

            if(resultDto is null)            
                return NotFound();
            
            return Ok(resultDto);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createRestaurantDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            if(!ModelState.IsValid)            
                return BadRequest(ModelState);
            
            var result = _restaurantService.CreateRestaurant(createRestaurantDto);

            return Created($"/api/restaurant/{result}",null);      
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteRestaurantById([FromRoute] int id)
        {
            var result = _restaurantService.DeleteRestaurant(id);

            if(result)
                return NoContent();

            return NotFound();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateRestaurantById([FromBody] UpdateRestaurantDto updateRestaurantDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = _restaurantService.UpdateRestaurant(id, updateRestaurantDto);

            if (!isUpdated)
                return NotFound();

            return Ok();
        }
    }
}
