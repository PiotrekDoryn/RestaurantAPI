using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
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

        public RestaurantController(RestaurantDBcontext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Return All Restaurants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Restaurant))]
        public ActionResult<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            var result = _dbContext.Restaurants.ToList();
            return Ok(result);
        }

        /// <summary>
        /// Return All Restaurant by Id number
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Restaurant))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Restaurant>> GetRestaurantById([FromRoute] int id)
        {
            var result = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
