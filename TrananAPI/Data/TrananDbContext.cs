using Microsoft.EntityFrameworkCore;
using TrananAPI.Models;

namespace TrananAPI.Data;

public class TrananDbContext : DbContext
{
    public TrananDbContext(DbContextOptions<TrananDbContext> options)
        : base(options) { }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     string dbFilePath =
    //         @"C:\Users\angel\Documents\SUVNET22\OOP2\INLÄMNINGAR\bio-tranan-Radagastno1\TrananAPI\Data\tranandatabase.db";
    //     optionsBuilder.UseSqlite($"Data Source={dbFilePath}");
    // }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDb");
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Movie>()
            .HasMany(m => m.Actors)
            .WithMany(a => a.Movies)
            .UsingEntity(x => x.ToTable("actor_to_movie"));

        modelBuilder
            .Entity<Movie>()
            .HasMany(m => m.Directors)
            .WithMany(d => d.Movies)
            .UsingEntity(x => x.ToTable("director_to_movie"));

    //     modelBuilder.Entity<Movie>().HasMany(m => m.MovieScreenings).WithOne(a => a.Movie);

    //     modelBuilder.Entity<Theater>().HasMany(m => m.MovieScreenings).WithOne(a => a.Theater);

        modelBuilder.Entity<Theater>().HasMany(m => m.Seats).WithOne(a => a.Theater);

    //     modelBuilder
    //         .Entity<Seat>()
    //         .HasMany(m => m.Reservations)
    //         .WithMany(r => r.Seats)
    //         .UsingEntity(x => x.ToTable("seat_to_reservation"));

    //     modelBuilder
    //         .Entity<MovieScreening>()
    //         .HasMany(m => m.Reservations)
    //         .WithOne(a => a.MovieScreening);

        // modelBuilder.Entity<Customer>().HasMany(m => m.Reservations).WithOne(a => a.Customer);
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors{get;set;}
    public DbSet<Director> Directors{get;set;}
    // public DbSet<MovieScreening> MovieScreenings { get; set; }
    // public DbSet<Customer> Customers { get; set; }
    // public DbSet<Admin> Admins { get; set; }
    // public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Theater> Theaters { get; set; }
}
