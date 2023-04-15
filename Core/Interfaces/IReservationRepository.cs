using Core.Models;

namespace Core.Interface;

public interface IReservationRepository
{
    public Task<List<Reservation>> GetByScreeningIdAsync(int screeningId);
}
