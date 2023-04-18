using Core.Interface;
using Hangfire;

namespace Core.Workers;

public class HangfireWorker
{
    private readonly IReservationService _reservationService;

    public HangfireWorker(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public void CallRemoveUnvalidReservations()
    {
        _reservationService.RemoveUnvalidReservations().Wait();
    }

    public void DailyWork()
    {
        Console.WriteLine("DAILY WORK ANROPAS!!!!!!!!!!!!!!!!!");
        BackgroundJob.Enqueue(() => CallRemoveUnvalidReservations());
    }
}
