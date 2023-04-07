using Microsoft.AspNetCore.Mvc;
using TrananAPI.Data;
using TrananAPI.DTO;

namespace TrananAPI.Controllers;

[ApiController]
[Route("reservation")]
public class ReservationController : ControllerBase
{
    private readonly ReservationRepository _repository;

    public ReservationController(ReservationRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
    {
        try
        {
            var reservations = await _repository.GetReservations();
            return Ok(reservations);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    // [HttpGet("{id:int}")]
    // public async Task<ActionResult<TheaterDTO>> GetTheaterById(int id) 
    // { 
    //     try
    //     {
    //         var theater = _seedData.GetTheaterById(id);
    //         return Ok(theater);
    //     }
    //     catch(Exception)
    //     {
    //         return NotFound();
    //     }
    // }

    [HttpPost]
    public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDTO)
    {
        try
        {
            var addedReservation = _repository.CreateReservation(reservationDTO);
            return Ok(addedReservation);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
    // [HttpPut]
    // public async Task<IActionResult> PutTheater(TheaterDTO theaterDTO)
    // {
    //     try
    //     {
    //         await _seedData.UpdateTheater(theaterDTO);
    //         return Ok(); //fixa
    //     }
    //     catch(Exception)
    //     {
    //         return NotFound();
    //     }
    // }
    // [HttpDelete]
    // public async Task<ActionResult> DeleteTheater(TheaterDTO theaterDTO)
    // {
    //     try
    //     {
    //         await _seedData.DeleteTheater(theaterDTO);
    //         return Ok();
    //     }
    //     catch(Exception)
    //     {
    //         return NotFound();
    //     }
    // }
}
