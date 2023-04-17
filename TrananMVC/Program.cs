using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Core.Data.TrananDbContext>(options =>
    options.UseSqlite("Data Source=c:\\Users\\angel\\Documents\\SUVNET22\\OOP2\\INLÄMNINGAR\\bio-tranan-Radagastno1\\Core\\tranandatabase.db"));

builder.Services.AddScoped<Core.Interface.IRepository<Core.Models.Movie>,Core.Data.Repository.MovieRepository>();
builder.Services.AddScoped<Core.Interface.IRepository<Core.Models.MovieScreening>,Core.Data.Repository.MovieScreeningRepository>();
builder.Services.AddScoped<Core.Interface.IRepository<Core.Models.Reservation>,Core.Data.Repository.ReservationRepository>();
builder.Services.AddScoped<Core.Interface.IRepository<Core.Models.Theater>,Core.Data.Repository.TheaterRepository>();
builder.Services.AddScoped<Core.Interface.IActorRepository,Core.Data.Repository.ActorRepository>();
builder.Services.AddScoped<Core.Interface.ISeatRepository,Core.Data.Repository.SeatRepository>();
builder.Services.AddScoped<Core.Interface.IRepository<Core.Models.Review>,Core.Data.Repository.ReviewRepository>();
builder.Services.AddScoped<Core.Interface.IMovieScreeningRepository,Core.Data.Repository.MovieScreeningRepository>();
builder.Services.AddScoped<Core.Interface.IReservationRepository,Core.Data.Repository.ReservationRepository>();

builder.Services.AddScoped<Core.Interface.IService<Core.Models.Movie>,Core.Services.MovieService>();
builder.Services.AddScoped<Core.Interface.IService<Core.Models.MovieScreening>,Core.Services.MovieScreeningService>();
builder.Services.AddScoped<Core.Interface.IService<Core.Models.Theater>,Core.Services.TheaterService>();
builder.Services.AddScoped<Core.Services.SeatService>();
builder.Services.AddScoped<Core.Interface.IService<Core.Models.Reservation>,Core.Services.ReservationService>();
builder.Services.AddScoped<Core.Interface.IService<Core.Models.Review>,Core.Services.ReviewService>();
builder.Services.AddScoped<Core.Interface.IMovieScreeningService,Core.Services.MovieScreeningService>();
builder.Services.AddScoped<Core.Interface.IReservationService,Core.Services.ReservationService>();
builder.Services.AddScoped<Core.Interface.IReviewService,Core.Services.ReviewService>();

builder.Services.AddScoped<TrananMVC.Service.MovieScreeningService>();
builder.Services.AddScoped<TrananMVC.Interface.IMovieService,TrananMVC.Service.MovieService>();
builder.Services.AddScoped<TrananMVC.Service.ReservationService>();
builder.Services.AddScoped<TrananMVC.Interface.IMovieTrailerService,TrananMVC.ApiServices.MovieTrailerService>();
builder.Services.AddScoped<TrananMVC.Service.ReviewService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Bio}/{action=Index}/{id?}");

app.Run();
