using Microsoft.AspNetCore.Mvc;
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
    private readonly IService<ReservationDTO, ReservationDTO> _reservationService;
    private readonly IReservationService _reservationService2;

    public ReservationController(IService<ReservationDTO, ReservationDTO> reservationService, IReservationService reservationService2)
    {
        _reservationService = reservationService;
        _reservationService2 = reservationService2;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDTO>>> Get()
    {
        try
        {
            var reservations = await _reservationService.Get();
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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReservationDTO>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("screening/{screeningId:int}")]
    public async Task<ActionResult<List<ReservationDTO>>> GetByScreeningId(int screeningId)
    {
        try
        {
            var reservations = await _reservationService2.GetByScreeningId(
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
            var addedReservation = await _reservationService.Create(reservationDTO);
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
            var updatedReservation = await _reservationService.Update(reservationDTO);
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
            await _reservationService.DeleteById(id);
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
            var updatedReservation = await _reservationService2.CheckInReservation(code);
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
