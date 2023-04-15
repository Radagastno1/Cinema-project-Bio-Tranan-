using Core.Interface;
using Core.Models;

namespace Core.Services;

public class SeatCoreService
{
    private static ISeatRepository _seatRepository;
    public SeatCoreService(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }
    public static async Task<List<Seat>> GenerateSeatsFromIdsAsync(List<int>seatIds)
    {
        List<Seat>seats = new();
        foreach(int id in seatIds)
        {
            var seat = await _seatRepository.GetSeatByIdAsync(id);
            seats.Add(seat);
        }
        return seats;
    }
}