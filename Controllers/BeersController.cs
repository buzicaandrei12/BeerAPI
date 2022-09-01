using BeerAPI.Interfaces;
using BeerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BeerAPI.Controllers
{
    [ApiController]
    [Route("api/beers")]
    public class BeersController : ControllerBase
    {
        private readonly IBeersProvider _beersProvider;

        public BeersController(IBeersProvider beersProvider) => _beersProvider = beersProvider;

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _beersProvider.Delete(id);
            return Ok();
        }

        [HttpPut("rate/{id}/{stars}")]
        public async Task<IActionResult> Rate(int id, int stars)
        {
            var beer = await _beersProvider.Rate(id, stars);
            return Ok(beer);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Beer beer)
        {
            var result = await _beersProvider.Add(beer);
            if (result.Success)
            {
                return Ok(result.Beers);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _beersProvider.GetBeersAsync();
            if (result.Success)
            {
                return Ok(result.Beers);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _beersProvider.GetBeersAsync(name);
            if (result.Success)
            {
                return Ok(result.Beers);
            }
            return NotFound(result.ErrorMessage);
        }
    }
}
