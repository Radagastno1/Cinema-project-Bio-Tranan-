using Microsoft.AspNetCore.Mvc;
using TrananAPI.Service;
using TrananAPI.DTO;

namespace TrananAPI.Controllers;

[ApiController]
[Route("reservation")]
public class ReservationController : ControllerBase
{
    private readonly ReservationService _reservationService;

    public ReservationController(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
    {
        try
        {
            var reservations = await _reservationService.GetReservations();
            return Ok(reservations);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpGet("{screeningId:int}")]
    public async Task<ActionResult<List<ReservationDTO>>> GetReservationsByScreeningId(int screeningId) 
    { 
        try
        {
            var reservations = _reservationService.GetReservationsByMovieScreening(screeningId);
            return Ok(reservations);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO)
    {
        try
        {
            var addedReservation = _reservationService.CreateReservation(reservationDTO);
            return Ok(addedReservation);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpPut]
    public async Task<ActionResult<ReservationDTO>> PutReservation(ReservationDTO reservationDTO)
    {
        try
        {
            var updatedReservation = await _reservationService.UpdateReservation(reservationDTO);
            return Ok(updatedReservation); 
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReservationById(int id)
    {
        try
        {
            await _reservationService.DeleteReservation(id);
            return Ok();
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
}
