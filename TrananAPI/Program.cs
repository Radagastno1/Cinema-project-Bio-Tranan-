using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


//ATT GÖRA!!!!!
//FELSÖK VARFÖR DET INTE GÅR ATT POSTA EN MOVIESCREENING? något värde är null på vägen som ej ska vara det



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<TrananAPI.Data.MovieSeedData>();
builder.Services.AddScoped<TrananAPI.Data.TheaterSeedData>();
builder.Services.AddScoped<TrananAPI.Data.MovieScreeningSeedData>();
builder.Services.AddDbContext<TrananAPI.Data.TrananDbContext>
(o => o.UseInMemoryDatabase("InMemoryDb"));

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
