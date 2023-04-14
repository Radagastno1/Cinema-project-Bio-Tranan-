using Core.Data.Repository;
using Core.Models;

namespace Core.Services;

public class SeatCoreService
{
    private static SeatRepository _seatRepository;
    public SeatCoreService(SeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }

    public static async Task<List<Seat>> GenerateSeatsFromIdsAsync(List<int>seatIds)
    {
        List<Seat>seats = new();
        foreach(int id in seatIds)
        {
            var seat = await _seatRepository.GetSeatById(id);
            seats.Add(seat);
        }
        return seats;
    }
}