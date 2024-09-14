using divar.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDivarService("eyJhbGciOiJSUzI1NiIsImtpZCI6InByaXZhdGVfa2V5XzIiLCJ0eXAiOiJKV1QifQ.eyJhcHBfc2x1ZyI6InNuYXBkcmFnb24tYmVyeWwtbGlmdGVyIiwiYXVkIjoic2VydmljZXByb3ZpZGVycyIsImV4cCI6MTczMTQwODk1NSwianRpIjoiYzA1MDNiMDMtNzFiZS0xMWVmLTg3NGUtYWUyNTllODQ5MmU0IiwiaWF0IjoxNzI2MjI0OTU1LCJpc3MiOiJkaXZhciIsInN1YiI6ImFwaWtleSJ9.CuORzX3ADW-BJy_Qogj7LOx_i8OwrwrcgQXQESqvNEWVWpjw99Y_KkTpUEd8iqCbJcIhqYCrmw8hPFnMN0pwJz-IGeHEy8mPgyutBfcTIl_e21XOa572OZlCOsrsqsgABo7a4soOLOJOK5NDO3eGX-PyyeJVEk_TsYOCGQYrOQoEsp7zEUIItBIDzQQk28Y-6wv8o2NwYqHZJ1PhbSFp7ff0__cQWI474HNots_NtnVM23SNOQLEErZyyNKxDTTGaXOWlnZEDPg5f-cigCKiMzLwFxTKRKtOLScKz3SCrxIQ27g7xbtIj-ibv9mzXY-Wc7DB7EKoeQlg18RVY-sRHQ");
builder.Services.AddDbContext<DivarDataContext>();
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
