using Microsoft.EntityFrameworkCore;
using Mission_6.Data;
using Mission_6.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MovieDb")));

var app = builder.Build();

// Ensure database is created and seeded.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
    db.Database.EnsureCreated();
    if (!db.Movies.Any())
    {
        db.Movies.AddRange(
            new Movie { Category = "Drama", Title = "The Shawshank Redemption", Year = 1994, Director = "Frank Darabont", Rating = "R", Edited = false },
            new Movie { Category = "Comedy", Title = "The Big Lebowski", Year = 1998, Director = "Joel Coen, Ethan Coen", Rating = "R", Edited = false },
            new Movie { Category = "Action/Adventure", Title = "Raiders of the Lost Ark", Year = 1981, Director = "Steven Spielberg", Rating = "PG", Edited = false }
        );
        db.SaveChanges();
    }
}

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

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();