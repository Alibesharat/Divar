using divar.DAL;
using divar.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DivarDataContext>();
builder.Services.AddSmsService();
builder.Services.ConfigureApplicationCookie(options =>
{
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(30); // Set expiration time
        options.SlidingExpiration = true; // Reset the expiration time on each request
        options.LoginPath = "/Experts/Login"; // Redirect to login page
        options.AccessDeniedPath = "/Home/AccessDenied"; // Redirect if access denied
});

builder.Services.Configure<DivarSetting>(builder.Configuration.GetSection("Divar"));
builder.Services.AddDivarService();



// Set default authentication scheme
builder.Services.AddAuthentication("Cookies")
        .AddCookie("Cookies", options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
            options.LoginPath = "/Experts/Login"; // Ensure this path is correct
            options.AccessDeniedPath = "/Home/AccessDenied"; // Redirect if access denied
        });


builder.Services.AddHttpContextAccessor(); 
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
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
