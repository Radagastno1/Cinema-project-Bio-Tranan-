using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Interface;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Data;

public class TrananDbContext : DbContext, ITrananDbContext
{
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Theater> Theaters { get; set; }
    public DbSet<MovieScreening> MovieScreenings { get; set; }
    public DbSet<Customer> Customers { get; set; }

    // // public DbSet<Admin> Admins { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public TrananDbContext(DbContextOptions<TrananDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public class TrananDbContextFactory : IDesignTimeDbContextFactory<TrananDbContext>
    {
        public TrananDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TrananDbContext>();
            optionsBuilder.UseSqlite(
                $"Data Source=path = C:/Users/angel/Documents/SUVNET22/OOP2/INLÄMNINGAR/bio-tranan-Radagastno1/Core/tranandatabase.db"
            );

            return new TrananDbContext(optionsBuilder.Options);
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            @"Data Source=C:\Users\angel\Documents\SUVNET22\OOP2\INLÄMNINGAR\bio-tranan-Radagastno1\Core\tranandatabase.db"
        );
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().HasMany(m => m.Actors).WithMany(a => a.Movies);

        modelBuilder.Entity<Movie>().HasMany(m => m.Directors).WithMany(d => d.Movies);

        modelBuilder
            .Entity<Movie>()
            .HasMany(m => m.MovieScreenings)
            .WithOne(s => s.Movie)
            .HasForeignKey(s => s.MovieId);

        modelBuilder
            .Entity<MovieScreening>()
            .HasOne(m => m.Theater)
            .WithMany(t => t.MovieScreenings);

        modelBuilder.Entity<Theater>().HasMany(m => m.Seats).WithOne(a => a.Theater);

        modelBuilder.Entity<Reservation>().HasMany(r => r.Seats).WithMany(s => s.Reservations);

        modelBuilder
            .Entity<MovieScreening>()
            .HasMany(m => m.Reservations)
            .WithOne(a => a.MovieScreening);

        modelBuilder.Entity<Customer>().HasMany(m => m.Reservations).WithOne(a => a.Customer);
        modelBuilder.Entity<Reservation>().HasOne(r => r.Customer).WithMany(c => c.Reservations);

        modelBuilder.Entity<Movie>()
        .HasMany(m => m.Reviews)
        .WithOne(r => r.Movie)
        .OnDelete(DeleteBehavior.Cascade);

    }
}
