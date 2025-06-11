using Microsoft.AspNetCore.Authentication.Cookies;
using PhamQuocCuong_SE1821_A01_FE.ApiServices;
using PhamQuocCuong_SE1821_A01_FE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("api", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7144/api/");
});

builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<NewsArticleService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<SystemAccountService>();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", opt =>
    {
        opt.LoginPath = "/auth/login";
        opt.LogoutPath = "/auth/logout";
        opt.ExpireTimeSpan = TimeSpan.FromDays(7);
        opt.SlidingExpiration = true;
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
