using IdentityServer.Extensions;
using IdentityServer.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//IdentityServer için eklenenler
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddIdentityWithExtension();

//Cookie iþlemleri
builder.Services.ConfigureApplicationCookie(opt =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "DefaultCookie";
    opt.LoginPath = new PathString("/Home/SignIn");
    opt.LogoutPath = new PathString("/Member/logout");
    opt.Cookie = cookieBuilder;
    opt.ExpireTimeSpan=TimeSpan.FromDays(3);
    opt.SlidingExpiration = true;// Kullanýcý 3 gün boyunca giriþ yaparsa tekrar 3 gün zaman tanýnýr.
});


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
app.UseAuthentication();// middleware doðrulama için eklendi
app.UseAuthorization();

app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
