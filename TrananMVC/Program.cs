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

builder.Services.AddScoped<Core.Services.MovieCoreService>();
builder.Services.AddScoped<Core.Services.MovieScreeningCoreService>();
builder.Services.AddScoped<Core.Services.TheaterCoreService>();
builder.Services.AddScoped<Core.Services.SeatCoreService>();
builder.Services.AddScoped<Core.Services.ReservationCoreService>();
builder.Services.AddScoped<Core.Services.ReviewCoreService>();

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
