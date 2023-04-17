using Microsoft.EntityFrameworkCore;
using Core.Models;
using TrananAPI.DTO;
using TrananAPI.Interface;
using TrananAPI.Service.Mapper;

namespace TrananAPI.Services;

public class ReservationService : IService<ReservationDTO, ReservationDTO>, IReservationService
{
    private readonly Core.Interface.IService<Reservation> _coreReservationService;
    private readonly Core.Interface.IReservationService _coreReservationService2;

    public ReservationService(
        Core.Interface.IService<Reservation> coreReservationService,
        Core.Interface.IReservationService coreReservationService2
    )
    {
        _coreReservationService = coreReservationService;
        _coreReservationService2 = coreReservationService2;
    }

    public async Task<IEnumerable<ReservationDTO>> Get()
    {
        try
        {
            var reservations = await _coreReservationService.Get();
            var reservationDTOs = reservations.Select(r => Mapper.GenerateReservationDTO(r));
            if (reservationDTOs == null)
            {
                return Enumerable.Empty<ReservationDTO>();
            }
            return reservationDTOs;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
   
    public async Task<IEnumerable<ReservationDTO>> GetByScreeningId(
        int screeningId
    )
    {
        try
        {
            var reservationsForScreening =
                await _coreReservationService2.GetReservationsByMovieScreening(screeningId);
            var reservationDTOs = reservationsForScreening.Select(
                r => Mapper.GenerateReservationDTO(r)
            );
            if (reservationDTOs == null)
            {
                return Enumerable.Empty<ReservationDTO>();
            }
            return reservationDTOs;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ReservationDTO> Create(ReservationDTO reservationDTO)
    {
        try
        {
            var newReservation = await Mapper.GenerateReservation(reservationDTO);
            var addedReservation = await _coreReservationService.Create(newReservation);
            var addedReservationDTO = Mapper.GenerateReservationDTO(addedReservation);
            if (addedReservationDTO == null)
            {
                return new ReservationDTO();
            }
            return addedReservationDTO;
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

    public async Task<ReservationDTO> Update(ReservationDTO reservationDTO)
    {
        try
        {
            var reservationToUpdate = await Mapper.GenerateReservation(reservationDTO);
            var updatedReservation = await _coreReservationService.Update(reservationToUpdate);
            var updatedReservationDTO = Mapper.GenerateReservationDTO(reservationToUpdate);
            if (updatedReservationDTO == null)
            {
                return new ReservationDTO();
            }
            return updatedReservationDTO;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteById(int id)
    {
        try
        {
            await _coreReservationService.DeleteById(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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

    Task<ReservationDTO> IService<ReservationDTO, ReservationDTO>.GetById(int id)
    {
        throw new NotImplementedException();
    }
}
