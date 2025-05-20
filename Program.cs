using Microsoft.EntityFrameworkCore;
using NumberConverterApp.DataB;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); 


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


app.MapControllers(); 
app.MapDefaultControllerRoute(); 
app.MapRazorPages(); 

app.Run();