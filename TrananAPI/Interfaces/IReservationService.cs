using TrananAPI.DTO;

namespace TrananAPI.Interface;

public interface IReservationService
{
      public Task<IEnumerable<ReservationDTO>> GetByScreeningId(
        int screeningId);


    public Task<ReservationDTO> CheckInReservation(int code);

}