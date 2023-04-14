using Microsoft.EntityFrameworkCore;
using Core.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Data;

public class TrananDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<MovieScreening> MovieScreenings { get; set; }
    public DbSet<Customer> Customers { get; set; }

    //public DbSet<Admin> Admins { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Theater> Theaters { get; set; }

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
            optionsBuilder.UseSqlite($"Data Source=tranandatabase.db");

            return new TrananDbContext(optionsBuilder.Options);
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=tranandatabase.db");
        optionsBuilder.EnableSensitiveDataLogging();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite($"Data Source=tranandatabase.db");
        }
    }

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

        modelBuilder
            .Entity<Movie>()
            .HasMany(m => m.MovieScreenings)
            .WithOne(s => s.Movie)
            .HasForeignKey(s => s.MovieId);

        modelBuilder.Entity<Theater>().HasMany(m => m.MovieScreenings).WithOne(a => a.Theater);
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
    }
}
