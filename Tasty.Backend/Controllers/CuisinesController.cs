using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Tasty.Backend.Data;
using Tasty.Backend.Models;

namespace Tasty.Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CuisinesController : ControllerBase
    {

        private readonly ILogger<CuisinesController> _logger;
        private readonly AppDataContext _context;

        public CuisinesController(ILogger<CuisinesController> logger, AppDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCuisines()
        {
            var cuisines = await _context.Cuisines
                .ToListAsync();
            return StatusCode((int)HttpStatusCode.OK, cuisines);
        }
        
        [HttpPost()]
        public async Task<IActionResult> AddCuisine([FromBody] Cuisine cuisine)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _context.Cuisines.AddRangeAsync(cuisine);
                await _context.SaveChangesAsync();
                return StatusCode((int)HttpStatusCode.Created, cuisine);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCuisine([FromRoute] int id)
        {
            try
            {
                var cuisine = await _context.Cuisines.FirstOrDefaultAsync(c => c.Id == id);
                if (cuisine == null)
                {
                    return NotFound();
                }

                _context.Cuisines.Remove(cuisine);
                await _context.SaveChangesAsync();
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> DeleteCuisine([FromRoute] int id, [FromBody] Cuisine cuisine)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var cuisineToUpdate = await _context.Cuisines.FirstOrDefaultAsync(c => c.Id == id);
                if (cuisineToUpdate == null)
                {
                    return NotFound();
                }

                cuisineToUpdate.Name = cuisine.Name;
                cuisineToUpdate.Restaurants = cuisine.Restaurants;

                await _context.SaveChangesAsync();
                return StatusCode((int)HttpStatusCode.OK, cuisineToUpdate);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}