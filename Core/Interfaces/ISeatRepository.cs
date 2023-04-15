using Core.Models;

namespace Core.Interface;

public interface ISeatRepository
{

    public Task<Seat> GetSeatByIdAsync(int id);
}