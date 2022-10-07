using BCNPortal.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using BCNPortal.Areas.Identity.Data;
using BCNPortal.Utility;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcnUser;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// Revisar builder.Services.UseInjection(), si hay q comentarearlo para la construccion de las 1ras tablas de BD, y despues descomentarearlo...probar a ver
builder.Services.UseInjection();


builder.Services.AddAntiforgery(opts =>
{
    opts.HeaderName = "__RequestVerificationToken";
});
string connectionString;
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
else
    connectionString = builder.Configuration.GetConnectionString("ProductionConnection") ?? throw new InvalidOperationException("Connection string 'ProductionConnection' not found.");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version("8.0.30"))));
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseMySql("server = localhost; port = 3306; database = BcnPortal; user = root; password = Cardinals25", new MySqlServerVersion(new Version("8.0.30"))));




builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
