using Microsoft.AspNetCore.Mvc;
using TrananAPI.DTO;
using Core.Models;
using TrananAPI.Service.Mapper;
using TrananAPI.Interface;

namespace TrananAPI.Controllers;

[ApiController]
[Route("reservation")]
public class ReservationController
    : ControllerBase,
        IController<ReservationDTO, ReservationDTO>,
        IReservationController
{
    private readonly Core.Interface.IService<Reservation> _coreReservationService;
    private readonly Core.Interface.IReservationService _coreReservationService2;

    public ReservationController(
        Core.Interface.IService<Reservation> coreReservationService,
        Core.Interface.IReservationService coreReservationService2
    )
    {
        _coreReservationService = coreReservationService;
        _coreReservationService2 = coreReservationService2;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDTO>>> Get()
    {
        try
        {
            var reservations = await _coreReservationService.Get();
            if (reservations == null)
            {
                return BadRequest("Failed to get reservations.");
            }
            return Ok(reservations.Select(r => Mapper.GenerateReservationDTO(r)));
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
            var reservations = await _coreReservationService2.GetReservationsByMovieScreening(
                screeningId
            );
            if (reservations == null)
            {
                return BadRequest("Failed to get reservations by movie screening id.");
            }
            return Ok(reservations.Select(r => Mapper.GenerateReservationDTO(r)));
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
            var newReservation = Mapper.GenerateReservation(reservationDTO);
            var addedReservation = await _coreReservationService.Create(newReservation);
            if (addedReservation == null)
            {
                return BadRequest("Failed to create reservation.");
            }
            return CreatedAtAction(
                nameof(Get),
                null,
                Mapper.GenerateReservationDTO(addedReservation)
            );
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
            var reservationToUpdate = Mapper.GenerateReservation(reservationDTO);
            var updatedReservation = await _coreReservationService.Update(reservationToUpdate);
            if (updatedReservation == null)
            {
                return BadRequest("Failed to update reservation.");
            }
            return Ok(Mapper.GenerateReservationDTO(updatedReservation));
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
            await _coreReservationService.DeleteById(id);
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
            var updatedReservation = await _coreReservationService2.CheckInReservationByCode(code);
            if (updatedReservation == null)
            {
                return BadRequest("Failed to check in reservation.");
            }
            return Ok(Mapper.GenerateReservationDTO(updatedReservation));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
