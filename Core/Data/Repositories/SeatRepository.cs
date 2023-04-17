using Core.Models;
using Core.Interface;

namespace Core.Data.Repository;

public class SeatRepository : ISeatRepository
{
    private readonly TrananDbContext _dbContext;

    public SeatRepository(TrananDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Seat> GetSeatByIdAsync(int id)
    {
        var seat = await _dbContext.Seats.FindAsync(id);
        return seat; 
    }
}