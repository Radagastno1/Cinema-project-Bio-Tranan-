using Microsoft.AspNetCore.Mvc;
using TrananAPI.Services;
using TrananAPI.DTO;
using TrananAPI.Interface;

namespace TrananAPI.Controllers;

[ApiController]
[Route("reservation")]
public class ReservationController
    : ControllerBase,
        IController<ReservationDTO, ReservationDTO>,
        IReservationController
{
    private readonly ReservationService _reservationService;

    public ReservationController(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDTO>>> Get()
    {
        try
        {
            var reservations = await _reservationService.GetReservations();
            if (reservations == null)
            {
                return BadRequest("Failed to get reservations.");
            }
            return Ok(reservations);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("{id:int}")] //fixa
    public async Task<ActionResult<ReservationDTO>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{screeningId:int}")]
    public async Task<ActionResult<List<ReservationDTO>>> GetByScreeningId(int screeningId)
    {
        try
        {
            var reservations = await _reservationService.GetReservationsByMovieScreening(
                screeningId
            );
            if (reservations == null)
            {
                return BadRequest("Failed to get reservations by movie screening id.");
            }
            return Ok(reservations);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDTO>> Post(ReservationDTO reservationDTO)
    {
        try
        {
            var addedReservation = await _reservationService.CreateReservation(reservationDTO);
            if (addedReservation == null)
            {
                return BadRequest("Failed to create reservation.");
            }
            return CreatedAtAction(nameof(Get), null, addedReservation);
        }
        catch (InvalidOperationException e)
        {
            if (e.Message == "Seat is not available.")
            {
                return BadRequest(e.Message);
            }
            if (e.Message == "Seat not found.")
            {
                return BadRequest(e.Message);
            }
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<ReservationDTO>> Put(ReservationDTO reservationDTO)
    {
        try
        {
            var updatedReservation = await _reservationService.UpdateReservation(reservationDTO);
            if (updatedReservation == null)
            {
                return BadRequest("Failed to update reservation.");
            }
            return Ok(updatedReservation);
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
            await _reservationService.DeleteReservation(id);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost("/check-in/{code:int}")]
    public async Task<ActionResult<ReservationDTO>> CheckInReservation(int code)
    {
        try
        {
            var updatedReservation = await _reservationService.CheckInReservation(code);
            if (updatedReservation == null)
            {
                return BadRequest("Failed to check in reservation.");
            }
            return Ok(updatedReservation);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
