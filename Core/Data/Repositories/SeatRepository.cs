using Core.Models;

namespace Core.Data.Repository;

public class SeatRepository 
{
    private readonly TrananDbContext _dbContext;

    public SeatRepository(TrananDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Seat> GetSeatById(int id)
    {
        var seat = await _dbContext.Seats.FindAsync(id);
        return seat; //felchecka
    }
}