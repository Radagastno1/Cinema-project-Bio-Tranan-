using Microsoft.AspNetCore.Mvc;
using TrananAPI.Service;
using TrananAPI.DTO;

namespace TrananAPI.Controllers;

[ApiController]
[Route("theater")]
public class TheaterController : ControllerBase
{
    private readonly TheaterService _theaterService;

    public TheaterController(TheaterService theaterService)
    {
        _theaterService = theaterService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TheaterDTO>>> GetTheaters()
    {
        try
        {
            var theaters = await _theaterService.GetTheaters();
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
            var theater = _theaterService.GetTheaterById(id);
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
            var addedTheater = _theaterService.CreateTheater(theaterDTO);
            return Ok(addedTheater);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpPut]
    public async Task<ActionResult<TheaterDTO>> PutTheater(TheaterDTO theaterDTO)
    {
        try
        {
            var updatedTheater = await _theaterService.UpdateTheater(theaterDTO);
            return Ok(updatedTheater); 
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTheater(int id)
    {
        try
        {
            await _theaterService.DeleteTheaterById(id);
            return Ok();
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
}
