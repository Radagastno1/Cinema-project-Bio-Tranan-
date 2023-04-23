using Microsoft.AspNetCore.Mvc;
using TrananAPI.DTO;
using Core.Models;
using TrananAPI.Interface;
using TrananAPI.Service.Mapper;

namespace TrananAPI.Controllers;

[ApiController]
[Route("theater")]
public class TheaterController : ControllerBase, IController<TheaterDTO, TheaterDTO>
{
    private readonly Core.Interface.IService<Theater> _coreTheaterService;

    public TheaterController(Core.Interface.IService<Theater> coreTheaterService)
    {
        _coreTheaterService = coreTheaterService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TheaterDTO>>> Get()
    {
        try
        {
            var theaters = await _coreTheaterService.Get();
            if (theaters == null)
            {
                return BadRequest("Failed to get theaters.");
            }
            return Ok(theaters.Select(t => Mapper.GenerateTheaterDTO(t)));
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
            var theater = await _coreTheaterService.GetById(id);
            if (theater == null)
            {
                return BadRequest("Failed to get theater by id.");
            }
            return Ok(Mapper.GenerateTheaterDTO(theater));
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
            var theaterToCreate = Mapper.GenerateTheater(theaterDTO);
            var addedTheater = await _coreTheaterService.Create(theaterToCreate);
            if (addedTheater == null)
            {
                return BadRequest("Failed to create theater.");
            }
            return CreatedAtAction(nameof(GetById), new { id = addedTheater.TheaterId });
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
            var theaterToUpdate = Mapper.GenerateTheater(theaterDTO);
            var updatedTheater = await _coreTheaterService.Update(theaterToUpdate);
            if (updatedTheater == null)
            {
                return BadRequest("Failed to update theater.");
            }
            return Ok(Mapper.GenerateTheaterDTO(updatedTheater));
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
            await _coreTheaterService.DeleteById(id);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
