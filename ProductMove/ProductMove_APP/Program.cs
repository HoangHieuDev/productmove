using Microsoft.AspNetCore.Authentication.Cookies;
using RestSharp;

public class Program
{
    public static RestClient Client { get; set; } = null!;
    public static RestClient Client_2 { get; set; } = null!;
    public static IConfiguration Config { get; private set; } = null!;
    public static string RootPath { get; set; } = null!;
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.LoginPath = "/UserPages/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            });
        builder.Services.AddHttpClient();

        builder.Services.AddDistributedMemoryCache();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(1000);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        var app = builder.Build();
        Config = app.Configuration;
        Client = new RestClient(Config["APIServer"]);
        Client_2 = new RestClient(Config["APIServer2"]);
        RootPath = app.Environment.ContentRootPath;
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseAuthentication();
        app.MapRazorPages();
        app.UseSession();

        app.Run();
    }
}