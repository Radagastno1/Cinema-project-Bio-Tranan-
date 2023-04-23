using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

GlobalConfiguration.Configuration.UseMemoryStorage();

builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage();
});

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(
        options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                .Json
                .ReferenceLoopHandling
                .Ignore
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Core.Data.TrananDbContext>(
    options =>
        options.UseSqlite(
            "Data Source=c:\\Users\\angel\\Documents\\SUVNET22\\OOP2\\INLÄMNINGAR\\bio-tranan-Radagastno1\\Core\\tranandatabase.db"
        )
);

builder.Services.AddScoped<
    Core.Interface.IRepository<Core.Models.Movie>,
    Core.Data.Repository.MovieRepository
>();
builder.Services.AddScoped<
    Core.Interface.IRepository<Core.Models.MovieScreening>,
    Core.Data.Repository.MovieScreeningRepository
>();
builder.Services.AddScoped<
    Core.Interface.IRepository<Core.Models.Reservation>,
    Core.Data.Repository.ReservationRepository
>();
builder.Services.AddScoped<
    Core.Interface.IRepository<Core.Models.Theater>,
    Core.Data.Repository.TheaterRepository
>();
builder.Services.AddScoped<Core.Interface.IActorRepository, Core.Data.Repository.ActorRepository>();
builder.Services.AddScoped<Core.Interface.ISeatRepository, Core.Data.Repository.SeatRepository>();
builder.Services.AddScoped<
    Core.Interface.IRepository<Core.Models.Review>,
    Core.Data.Repository.ReviewRepository
>();
builder.Services.AddScoped<
    Core.Interface.IMovieScreeningRepository,
    Core.Data.Repository.MovieScreeningRepository
>();
builder.Services.AddScoped<
    Core.Interface.IReservationRepository,
    Core.Data.Repository.ReservationRepository
>();

builder.Services.AddScoped<
    Core.Interface.IService<Core.Models.Movie>,
    Core.Services.MovieService
>();
builder.Services.AddScoped<
    Core.Interface.IService<Core.Models.MovieScreening>,
    Core.Services.MovieScreeningService
>();
builder.Services.AddScoped<
    Core.Interface.IService<Core.Models.Theater>,
    Core.Services.TheaterService
>();
builder.Services.AddScoped<Core.Services.SeatService>();
builder.Services.AddScoped<
    Core.Interface.IService<Core.Models.Reservation>,
    Core.Services.ReservationService
>();
builder.Services.AddScoped<
    Core.Interface.IService<Core.Models.Review>,
    Core.Services.ReviewService
>();
builder.Services.AddScoped<
    Core.Interface.IMovieScreeningService,
    Core.Services.MovieScreeningService
>();
builder.Services.AddScoped<Core.Interface.IReservationService, Core.Services.ReservationService>();
builder.Services.AddScoped<Core.Interface.IReviewService, Core.Services.ReviewService>();

builder.Services.AddScoped<Core.Workers.HangfireWorker>();

var serviceProvider = builder.Services.BuildServiceProvider();

GlobalConfiguration.Configuration.UseActivator(new Core.Workers.HangfireActivator(serviceProvider));

var app = builder.Build();

app.UseHangfireDashboard();
app.UseHangfireServer();

serviceProvider.GetService<Core.Workers.HangfireWorker>().DailyWork();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
