using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;

var builder = WebApplication.CreateBuilder(args);

//connect database to postgreSQL using defaultconnection in appsettings.json
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(
        //add connectionstring from appsettings.json
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout= TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential= true;
});

//datetime fix
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "Areas",
	pattern: "{area:exists}/{controller=Products}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "products",
    pattern: "/products/{categorySlug?}",
    defaults: new {controller = "Products", action="Index"});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Add seed data to implement seeding to database table
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();
