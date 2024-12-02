using EventManagamentSystem.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the DbContext with dependency injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add the session service if needed for session-based data
builder.Services.AddSession();

// Add any other necessary services here (e.g., for authentication, logging)
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // For better error handling in production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Enable detailed errors and developer tools in development
    app.UseDeveloperExceptionPage();
}

// Middleware to redirect HTTP traffic to HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

// Add routing and session if needed
app.UseRouting();
app.UseAuthorization();
app.UseSession();  // Add this if your application requires session management

// Configure the endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "event",
    pattern: "Event/{action=Index}/{id?}",
    defaults: new { controller = "Event" });

app.MapControllerRoute(
    name: "category",
    pattern: "Category/{action=Index}/{id?}",
    defaults: new { controller = "Category" });

app.MapControllerRoute(
    name: "attendee",
    pattern: "Attendee/{action=Index}/{id?}",
    defaults: new { controller = "Attendee" });

app.MapControllerRoute(
    name: "feedback",
    pattern: "Feedback/{action=Index}/{id?}",
    defaults: new { controller = "Feedback" });

app.MapControllerRoute(
    name: "user",
    pattern: "User/{action=Index}/{id?}",
    defaults: new { controller = "User" });

// Map Razor Pages if needed (e.g., for additional pages)
app.MapRazorPages();

app.Run();
