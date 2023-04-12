using TrananMVC.Repository;
using TrananMVC.ViewModel;
using TrananMVC.Models;

namespace TrananMVC.Service;

public class ReservationService
{
    private readonly ReservationRepository _reservationRepository;

    public ReservationService(ReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<ReservationViewModel> CreateReservation(
        ReservationViewModel reservationViewModel
    )
    {
        try
        {
            var reservation = Mapper.GenerateReservation(reservationViewModel);
            var addedReservation = await _reservationRepository.PostReservation(reservation);
            return Mapper.GenerateReservationViewModel(addedReservation);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
