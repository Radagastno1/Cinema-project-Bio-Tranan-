using Microsoft.AspNetCore.Mvc;
using TrananAPI.DTO;

namespace TrananAPI.Interface;

public interface IReservationController

{
    public Task<ActionResult<ReservationDTO>> CheckInReservation(int code);
}
