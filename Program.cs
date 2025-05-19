using Microsoft.EntityFrameworkCore;
using NumberConverterApp.DataB;

var builder = WebApplication.CreateBuilder(args);

// Register Controllers & Razor Pages (IMPORTANT)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // 🛠️ Enables Razor UI

// Register Database Context (Ensure correct DB connection)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Enable Middleware Required for UI
app.UseStaticFiles(); // alows CSS & JS loading
app.UseRouting();
app.UseAuthorization();

// Enable Razor UI Routes
app.MapControllers(); //  Keeps API working
app.MapDefaultControllerRoute(); // enables MVC routing
app.MapRazorPages(); // Ensures Razor UI works

app.Run();