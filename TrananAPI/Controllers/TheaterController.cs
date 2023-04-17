using Microsoft.AspNetCore.Mvc;
using TrananAPI.Services;
using TrananAPI.DTO;
using TrananAPI.Interface;

namespace TrananAPI.Controllers;

[ApiController]
[Route("theater")]
public class TheaterController : ControllerBase, IController<TheaterDTO, TheaterDTO>
{
    private readonly IService<TheaterDTO, TheaterDTO> _theaterService;

    public TheaterController(IService<TheaterDTO, TheaterDTO> theaterService)
    {
        _theaterService = theaterService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TheaterDTO>>> Get()
    {
        try
        {
            var theaters = await _theaterService.Get();
            if (theaters == null)
            {
                return BadRequest("Failed to get theaters.");
            }
            return Ok(theaters);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TheaterDTO>> GetById(int id)
    {
        try
        {
            var theater = await _theaterService.GetById(id);
            if (theater == null)
            {
                return BadRequest("Failed to get theater by id.");
            }
            return Ok(theater);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<TheaterDTO>> Post(TheaterDTO theaterDTO)
    {
        try
        {
            var addedTheater = await _theaterService.Create(theaterDTO);
            if (addedTheater == null)
            {
                return BadRequest("Failed to create theater.");
            }
            return CreatedAtAction(nameof(GetById), new { id = addedTheater.Id });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<TheaterDTO>> Put(TheaterDTO theaterDTO)
    {
        try
        {
            var updatedTheater = await _theaterService.Update(theaterDTO);
            if (updatedTheater == null)
            {
                return BadRequest("Failed to update theater.");
            }
            return Ok(updatedTheater);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            await _theaterService.DeleteById(id);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
