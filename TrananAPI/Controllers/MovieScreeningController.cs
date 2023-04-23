using TrananAPI.DTO;
using TrananAPI.Service.Mapper;
using TrananAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Core.Models;

namespace TrananAPI.Controllers;

[ApiController]
[Route("moviescreening")]
public class MovieScreeningController
    : ControllerBase,
        IController<MovieScreeningOutgoingDTO, MovieScreeningIncomingDTO>
{
    private readonly Core.Interface.IService<MovieScreening> _coreScreeningService;

    public MovieScreeningController(Core.Interface.IService<MovieScreening> coreScreeningService)
    {
        _coreScreeningService = coreScreeningService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieScreeningOutgoingDTO>>> Get()
    {
        try
        {
            var movieScreenings = await _coreScreeningService.Get();
            if (movieScreenings == null)
            {
                return BadRequest("Failed to get movie screenings.");
            }
            var screeningsDTOSs = movieScreenings
                .Select(s => Mapper.GenerateMovieScreeningOutcomingDTO(s))
                .ToList();
            return Ok(screeningsDTOSs);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieScreeningOutgoingDTO>> GetById(int id)
    {
        try
        {
            var movieScreening = await _coreScreeningService.GetById(id);
            if (movieScreening == null)
            {
                return BadRequest("Failed to get movie screening by id.");
            }
            return Ok(Mapper.GenerateMovieScreeningOutcomingDTO(movieScreening));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<MovieScreeningOutgoingDTO>> Post(
        MovieScreeningIncomingDTO movieScreeningDTO
    )
    {
        try
        {
            var newMovieScreening = new MovieScreening()
            {
                MovieId = movieScreeningDTO.MovieId,
                TheaterId = movieScreeningDTO.TheaterId,
                DateAndTime = movieScreeningDTO.DateAndTime
            };
            var createdMovieScreening = await _coreScreeningService.Create(newMovieScreening);
            if (newMovieScreening == null)
            {
                return BadRequest("Failed to create movie screening.");
            }
            return Ok(Mapper.GenerateMovieScreeningOutcomingDTO(createdMovieScreening));
        }
        catch (InvalidOperationException e)
        {
            if (e.Message == "Theater not available at chosen time and day.")
            {
                return BadRequest(e.Message);
            }
            else if (e.Message == "Movie has maximum amount moviescreenings.")
            {
                return BadRequest(e.Message);
            }
            return BadRequest(e.Message);
        }
        catch (NullReferenceException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<MovieScreeningOutgoingDTO>> Put(
        MovieScreeningIncomingDTO movieScreeningDTO
    )
    {
        try
        {
            var movieScreeningToUpdate = new Core.Models.MovieScreening()
            {
                MovieScreeningId = movieScreeningDTO.Id,
                DateAndTime = movieScreeningDTO.DateAndTime,
                MovieId = movieScreeningDTO.MovieId,
                TheaterId = movieScreeningDTO.TheaterId
            };
            var updatedMovieScreening = await _coreScreeningService.Update(movieScreeningToUpdate);
            if (updatedMovieScreening == null)
            {
                return BadRequest("Failed to update movie screening.");
            }
            return Ok(Mapper.GenerateMovieScreeningOutcomingDTO(updatedMovieScreening));
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
            await _coreScreeningService.DeleteById(id);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
