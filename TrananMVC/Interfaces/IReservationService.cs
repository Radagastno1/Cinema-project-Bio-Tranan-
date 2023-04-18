using TrananMVC.ViewModel;
namespace TrananMVC.Interface;

public interface IReservationService
{
     public Task<CreatedReservationViewModel> CreateReservation(
        ReservationViewModel reservationViewModel
    );
    public Task<bool> DeleteReservationById(int reservationId);
}
