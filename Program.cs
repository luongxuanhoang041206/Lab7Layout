using Lab7Layout.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Lab7LayoutContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/LXHHome/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapStaticAssets();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<Lab7LayoutContext>();
    await database.Database.EnsureCreatedAsync();
    await Lab7LayoutContext.SeedAsync(database);
}

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=LXHDashboard}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LXHHome}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
