using Microsoft.AspNetCore.Mvc; 
using TrananAPI.Data;
namespace TrananAPI.Controllers;

[ApiController]
[Route("theater")]
public class TheaterController : ControllerBase
{
      private readonly TheaterSeedData _seedData;

    public TheaterController(TheaterSeedData seedData)
    {
        _seedData = seedData;
    }
    [HttpGet]
    public async Task<IActionResult> GetTheaters()
    {
        try
        {
            var theaters = await _seedData.GetTheaters();
            return Ok(theaters);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
}