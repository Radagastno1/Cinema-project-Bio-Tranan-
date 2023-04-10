using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TrananAPI.Data.Repository.MovieRepository>();
builder.Services.AddScoped<TrananAPI.Data.Repository.TheaterRepository>();
builder.Services.AddScoped<TrananAPI.Data.Repository.MovieScreeningRepository>();
builder.Services.AddScoped<TrananAPI.Data.Repository.ReservationRepository>();
builder.Services.AddScoped<TrananAPI.Data.Repository.SeatRepository>();
builder.Services.AddScoped<TrananAPI.Service.MovieService>();
builder.Services.AddScoped<TrananAPI.Service.TheaterService>();
builder.Services.AddScoped<TrananAPI.Service.MovieScreeningService>();
builder.Services.AddScoped<TrananAPI.Service.ReservationService>();
builder.Services.AddScoped<TrananAPI.Service.SeatService>();
builder.Services.AddDbContext<TrananAPI.Data.TrananDbContext>
(o => o.UseSqlite("tranandatabase.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
