
using Microsoft.EntityFrameworkCore;
using brickwell.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("brickwellIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'brickwellIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<brickwellIdentityDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<brickwellIdentityDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<LegoDbContext>(options =>
//{
//options.UseSqlite(builder.Configuration["ConnectionStrings:LegoConnection"]);
//});

//builder.Services.AddScoped<ILegoRepository, EFLegoRepository>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
