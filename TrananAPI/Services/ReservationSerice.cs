using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Interface;
using TrananAPI.DTO;
using TrananAPI.Service;

namespace TrananAPI.Services;

public class ReservationService
{
    private readonly IService<Reservation> _coreReservationService;
    private readonly Core.Interface.IReservationService _coreReservationService2;

    public ReservationService(
        IService<Reservation> coreReservationService,
        IReservationService coreReservationService2
    )
    {
        _coreReservationService = coreReservationService;
        _coreReservationService2 = coreReservationService2;
    }

    public async Task<IEnumerable<ReservationDTO>> GetReservations()
    {
        var reservations = await _coreReservationService.Get();
        return reservations.Select(r => Mapper.GenerateReservationDTO(r));
    }

    public async Task<IEnumerable<ReservationDTO>> GetReservationsByMovieScreening(
        int movieScreeningId
    )
    {
        var reservationsForScreening =
            await _coreReservationService2.GetReservationsByMovieScreening(movieScreeningId);
        return reservationsForScreening.Select(r => Mapper.GenerateReservationDTO(r));
    }

    public async Task<ReservationDTO> CreateReservation(ReservationDTO reservationDTO)
    {
        try
        {
            var newReservation = await Mapper.GenerateReservation(reservationDTO);
            var addedReservation = await _coreReservationService.Create(newReservation);
            return Mapper.GenerateReservationDTO(addedReservation);
        }
        catch (InvalidOperationException e)
        {
            if (e.Message == "Seat is not available.")
            {
                throw new InvalidOperationException(e.Message);
            }
            if (e.Message == "Seat not found.")
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new InvalidOperationException();
        }
    }

    public async Task<ReservationDTO> UpdateReservation(ReservationDTO reservationDTO)
    {
        var reservationToUpdate = await Mapper.GenerateReservation(reservationDTO);
        var updatedReservation = await _coreReservationService.Update(reservationToUpdate);
        return Mapper.GenerateReservationDTO(reservationToUpdate);
    }

    public async Task DeleteReservation(int reservationId)
    {
        await _coreReservationService.DeleteById(reservationId);
    }

    public async Task<ReservationDTO> CheckInReservation(int code)
    {
        try
        {
            var checkedInReservation = await _coreReservationService2.CheckInReservationByCode(
                code
            );
            return Mapper.GenerateReservationDTO(checkedInReservation);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
