using TrananAPI.Data;
using TrananAPI.Models;

namespace TrananAPI.Service;

public class SeatService
{
    private static SeatRepository _seatRepository;
    public SeatService(SeatRepository seatRepository)
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