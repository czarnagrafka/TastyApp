using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Tasty.Backend.Data;

namespace Tasty.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {

        private readonly ILogger<RestaurantsController> _logger;
        private readonly AppDataContext _context;

        public RestaurantsController(ILogger<RestaurantsController> logger, AppDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _context.Restaurants
                .Include(e => e.Cuisines)
                .ToListAsync();
            return StatusCode((int)HttpStatusCode.OK, restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRestaurant([FromRoute] int id)
        {
            var restaurant = await _context.Restaurants
                .Include(e => e.Cuisines)
                .FirstOrDefaultAsync(restaurant => restaurant.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.OK, restaurant);
        }
    }
}