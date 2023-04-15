using Core.Models;
namespace Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
public interface ITrananDbContext
{
    // DbSet<Actor> Actors { get; set; }
    // DbSet<Director> Directors { get; set; }
    // DbSet<Movie> Movies { get; set; }
    // DbSet<Seat> Seats { get; set; }
    // DbSet<Theater> Theaters { get; set; }
    // DbSet<MovieScreening> MovieScreenings { get; set; }
    // DbSet<Customer> Customers { get; set; }
    // DbSet<Reservation> Reservations { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
