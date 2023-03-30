using Microsoft.AspNetCore.Mvc;
using TrananAPI.Data;
using TrananAPI.Models;

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
    public async Task<ActionResult<IEnumerable<Theater>>> GetTheaters()
    {
        try
        {
            var theaters = await _seedData.GetTheaters();
            return Ok(theaters);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Theater>> GetTheaterById(int id) 
    { 
        try
        {
            var theater = _seedData.GetTheaterById(id);
            return Ok(theater);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<Theater>> PostTheater(Theater theater)
    {
        try
        {
            var addedTheater = _seedData.CreateTheater(theater);
            return Ok(addedTheater);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpPut]
    public async Task<IActionResult> PutTheater(Theater theater)
    {
        try
        {
            await _seedData.UpdateTheater(theater);
            return Ok(); //fixa
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpDelete]
    public async Task<ActionResult> DeleteTheater(Theater theater)
    {
        try
        {
            await _seedData.DeleteTheater(theater);
            return Ok();
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
}
