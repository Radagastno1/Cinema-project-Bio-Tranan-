using Microsoft.AspNetCore.Mvc;
using TrananAPI.Data;
using TrananAPI.DTO;

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
    public async Task<ActionResult<IEnumerable<TheaterDTO>>> GetTheaters()
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
    public async Task<ActionResult<TheaterDTO>> GetTheaterById(int id) 
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
    public async Task<ActionResult<TheaterDTO>> PostTheater(TheaterDTO theaterDTO)
    {
        try
        {
            var addedTheater = _seedData.CreateTheater(theaterDTO);
            return Ok(addedTheater);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpPut]
    public async Task<IActionResult> PutTheater(TheaterDTO theaterDTO)
    {
        try
        {
            await _seedData.UpdateTheater(theaterDTO);
            return Ok(); //fixa
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpDelete]
    public async Task<ActionResult> DeleteTheater(TheaterDTO theaterDTO)
    {
        try
        {
            await _seedData.DeleteTheater(theaterDTO);
            return Ok();
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
}
