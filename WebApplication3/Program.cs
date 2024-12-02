using Microsoft.EntityFrameworkCore;
using WebApplication3.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();

var app = builder.Build();

app.MapGet("/api", () => new
{
    Message = "server is run"
});

app.MapGet("/api/ping", () => new
{
    Message = "pong"
});

app.MapGet("/country", async (ApplicationDbContext db) => await db.Countries.ToListAsync());

app.MapPost("/country", async (Country country, ApplicationDbContext db) =>
{
    await db.Countries.AddAsync(country);
    await db.SaveChangesAsync();
    return country;
});

app.MapGet("/country/{id:int}", async (int id, ApplicationDbContext db) => await db.Countries.FirstOrDefaultAsync(c => c.Id == id));

app.MapGet("/country/code/{code}", async (string code, ApplicationDbContext db) =>
{
    Country? country1 = await db.Countries.FirstOrDefaultAsync(c => c.ISO31661Alpha2Code == code);
    Country? country2 = await db.Countries.FirstOrDefaultAsync(c => c.ISO31661Alpha3Code == code);
    Country? country3 = await db.Countries.FirstOrDefaultAsync(c => c.ISO31661NumericCode == code);
    if (country1 != null)
    {
        return country1;
    }
    else if (country2 != null)
    {
        return country2;
    }
    else if (country3 != null)
    {
        return country3;
    }
    else
    {
        return null;
    }
});

app.Run();
