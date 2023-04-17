using Core.Models;

namespace Core.Interface;

public interface IReservationService
{
    public Task<IEnumerable<Reservation>> GetReservationsByMovieScreening(int movieScreeningId);
    public Task<Reservation> GetReservationByReservationCode(int reservationCode);
}
