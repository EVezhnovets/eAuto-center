using DiConfiguration;
using eAuto.Data.Context;
using eAuto.Data.Identity;
using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Web.Utilities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Stripe;

Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
				.Enrich.FromLogContext()
				.WriteTo.Console()
                .WriteTo.File("Logs/log.txt",
					rollingInterval: RollingInterval.Day,
					rollOnFileSizeLimit: true)
                .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddControllersWithViews();

var appConnection= builder.Configuration.GetConnectionString("eAutoCatalogConnection");
var identityConnection= builder.Configuration.GetConnectionString("eAutoIdentityConnection");

var diConfigurator = new DiConfigurator(identityConnection, appConnection, builder.Configuration);
diConfigurator.ConfigureServices(builder.Services, builder.Logging);
builder.Services.AddTransient<IImageManager, ImageManager>();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddAuthentication().AddFacebook(options =>
{
    //options.AppId = "";
    //options.AppSecret = "";
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

#region Auto Migration
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var context = scopedProvider.GetRequiredService<EAutoContext>();
        if (context.Database.IsSqlServer())
        {
            context.Database.Migrate();
        }
        await EntityContextSeed.SeedAsync(context);

        var identityContext = scopedProvider.GetRequiredService<IdentityContext>();
        if (identityContext.Database.IsSqlServer())
        {
            identityContext.Database.Migrate();
            var dbinitializer = scopedProvider.GetRequiredService<IDbInitializer>();
            dbinitializer.Initialize();
        }
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred adding migrations to DatabBase.");
    }
}
#endregion

app.UseSerilogRequestLogging();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/CarsCatalog/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=CarsCatalog}/{action=Index}/{id?}");

app.Run();