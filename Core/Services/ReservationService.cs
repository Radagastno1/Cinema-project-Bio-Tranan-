using Core.Models;
using Core.Interface;
using System.Net.Http.Json;

namespace Core.Services;

public class ReservationService : IService<Reservation>, IReservationService
{
    private IRepository<Reservation> _reservationRepository;
    private IReservationRepository _reservationByScreeningRepository;

    public ReservationService(
        IRepository<Reservation> reservationRepository,
        IReservationRepository reservationByScreeningRepository
    )
    {
        _reservationRepository = reservationRepository;
        _reservationByScreeningRepository = reservationByScreeningRepository;
    }

    public async Task RemoveUnvalidReservations()
    {
        var allReservations = await _reservationRepository.GetAsync();
        var unvalidReservations = allReservations
            .Where(r => DateTime.Now > r.MovieScreening.DateAndTime.AddDays(1))
            .ToList();
        foreach (var reservation in unvalidReservations)
        {
            await _reservationRepository.DeleteByIdAsync(reservation.ReservationId);
        }
    }

    public async Task<IEnumerable<Reservation>> Get()
    {
        try
        {
            var reservations = await _reservationRepository.GetAsync();
            if (reservations == null)
            {
                return Enumerable.Empty<Reservation>();
            }
            return reservations;
        }
        catch (Exception)
        {
            throw new Exception("Failed to get reservations from database.");
        }
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByMovieScreening(
        int movieScreeningId
    )
    {
        try
        {
            var reservationsForScreening =
                await _reservationByScreeningRepository.GetByScreeningIdAsync(movieScreeningId);
            if (reservationsForScreening == null)
            {
                return Enumerable.Empty<Reservation>();
            }
            return reservationsForScreening;
        }
        catch (Exception)
        {
            throw new Exception("Failed to get reservations by movie screening.");
        }
    }

    public async Task<Reservation> GetReservationByReservationCode(int reservationCode) //denna borde hämtas direkt från db, onödig process
    {
        try
        {
            var reservations = await _reservationRepository.GetAsync();
            if (reservations == null)
            {
                return null;
            }
            var reservation = reservations.Where(r => r.ReservationCode == reservationCode).First();
            if (reservation == null)
            {
                return null;
            }
            return reservation;
        }
        catch (Exception)
        {
            throw new Exception("Failed to get reservation by reservationcode.");
        }
    }

    public async Task<Reservation> CheckInReservationByCode(int code)
    {
        try
        {
            var checkedInReservation = await _reservationByScreeningRepository.CheckInReservation(
                code
            );
            if (checkedInReservation == null)
            {
                return null;
            }
            return checkedInReservation;
        }
        catch (Exception)
        {
            throw new Exception("Failed to check in reservation in database.");
        }
    }

    public async Task<Reservation> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Reservation> Create(Reservation reservation)
    {
        try
        {
            var completedReservation = await CreateReservationAsync(
                reservation.ReservationId,
                reservation.Price,
                reservation.MovieScreeningId,
                reservation.Customer,
                reservation.Seats,
                reservation.IsCheckedIn
            );

            var addedReservation = await _reservationRepository.CreateAsync(completedReservation);

            return addedReservation;
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
            throw new InvalidOperationException("Failed to create reservation.");
        }
    }

    public static async Task<Reservation> CreateReservationAsync(
        int reservationId,
        decimal price,
        int movieScreeningId,
        Customer customer,
        List<Seat> seats,
        bool isCheckedIn = false
    )
    {
        var reservation = new Reservation()
        {
            Price = price,
            MovieScreeningId = movieScreeningId,
            Customer = customer,
            Seats = seats,
            IsCheckedIn = isCheckedIn
        };

        reservation.ReservationCode = await GenerateReservationCodeAsync();
        return reservation;
    }

    private static async Task<int> GenerateReservationCodeAsync()
    {
        int randomNumber = await GetRandomNumberFromAPI();

        if (randomNumber == 0)
        {
            throw new Exception("Reservation code is unavailable. Please contact admin.");
        }
        return randomNumber;
    }

    private static async Task<int> GetRandomNumberFromAPI()
    {
        string url = "http://www.randomnumberapi.com/api/v1.0/random?min=100&max=1000";
        HttpClient httpClient = new();
        try
        {
            var randomNumberArray = await httpClient.GetFromJsonAsync<int[]>(url);
            int randomNumber = randomNumberArray[0];
            return randomNumber;
        }
        catch (HttpRequestException)
        {
            return 0;
        }
    }

    public async Task<Reservation> Update(Reservation reservation)
    {
        try
        {
            var updatedReservation = await _reservationRepository.UpdateAsync(reservation);
            if (updatedReservation == null)
            {
                return null;
            }
            return updatedReservation;
        }
        catch (Exception)
        {
            throw new Exception("Failed to update reservation.");
        }
    }

    public async Task DeleteById(int id)
    {
        try
        {
            await _reservationRepository.DeleteByIdAsync(id);
        }
        catch (Exception)
        {
            throw new Exception("Failed to delete reservation by id.");
        }
    }
}
