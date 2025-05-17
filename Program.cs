using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register Controllers & Views
builder.Services.AddControllersWithViews();

// Register Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Middleware Setup
app.UseRouting();
app.UseAuthorization();

// Map Routes
app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();