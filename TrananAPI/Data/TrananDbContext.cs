using Microsoft.EntityFrameworkCore;
using TrananAPI.Models;

namespace TrananAPI.Data;

public class TrananDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbFilePath = @"C:\Users\angel\Documents\SUVNET22\OOP2\INLÄMNINGAR\bio-tranan-Radagastno1\TrananAPI\Data\tranandatabase.db";
        optionsBuilder.UseSqlite($"Data Source={dbFilePath}");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieScreening> MovieScreenings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Theater> Theaters { get; set; }
}
