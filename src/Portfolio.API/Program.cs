using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Portfolio.API.Configuration;
using Portfolio.API.Exceptions;
using Portfolio.Application.DependencyInjection;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.DependencyInjection;
using Portfolio.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Logging configuration - simple starter configuration using built-in providers.
var loggerFactory = LoggerFactory.Create(logger =>
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
});
var startupLogger = loggerFactory.CreateLogger("Startup");

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();

// PortfolioConfig with extra validation against ""
builder.Services.AddOptions<PortfolioConfig>()
    .Bind(builder.Configuration.GetSection("PortfolioConfig"))
    .ValidateDataAnnotations()
    .Validate(x => !string.IsNullOrWhiteSpace(x.AdminUserName),
        "PortfolioConfig:AdminUserName is required.")
    .Validate(x => !string.IsNullOrWhiteSpace(x.AdminEmail),
        "PortfolioConfig:AdminEmail is required.")
    .Validate(x => !string.IsNullOrWhiteSpace(x.AdminPassword),
        "PortfolioConfig:AdminPassword is required.")
    .ValidateOnStart();

// Database Config
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration["ConnectionString"]));

// Dependency Injections
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// GlobalExceptionHandler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// AuthCookie Config
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "admin_auth";
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;

        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };

        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    });

// Antiforgery CSRF ( Cross-Site Request Forgery ) Token Config
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
    options.SuppressXFrameOptionsHeader = false;
});

try
{
    var app = builder.Build();
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Application started. Environment: {Environment}", app.Environment.EnvironmentName);

    await using (var scope = app.Services.CreateAsyncScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var config = scope.ServiceProvider.GetRequiredService<IOptions<PortfolioConfig>>().Value;

        logger.LogInformation("Initializing database and admin user");
        // TODO: Change it to migration
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();

        if (!db.WebsiteConfig.Any())
        {
            logger.LogInformation("No existing database found, creating default admin with default username:  {UserName}", config.AdminUserName);
            var passwordHasher = new PasswordHasher<WebsiteConfig>();
            var websiteConfig = new WebsiteConfig(config.AdminUserName, config.AdminEmail);
            websiteConfig.UpdatePasswordHash(passwordHasher.HashPassword(websiteConfig, config.AdminPassword));

            await db.WebsiteConfig.AddAsync(websiteConfig);
            await db.SaveChangesAsync();
            logger.LogInformation("Default admin user created successfully");
        }
        else
        {
            logger.LogInformation("Database already initialized, skipping admin user creation");
        }
    }

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "Portfolio API V1");
        });
    }

    // Request logging middleware - logs start/finish for the request.
    app.Use(async (context, next) =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var method = context.Request.Method;
        var path = context.Request.Path;
        logger.LogInformation("Started {Method} {Path} CorrelationId:{TraceId}", method, path, context.TraceIdentifier);
        var sw = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            await next();
            sw.Stop();
            logger.LogInformation("Finished {Method} {Path} {StatusCode} in {Elapsed}ms CorrelationId:{TraceId}", method, path, context.Response.StatusCode, sw.ElapsedMilliseconds, context.TraceIdentifier);
        }
        catch (Exception ex)
        {
            sw.Stop();
            throw;
        }
    });

    app.UseExceptionHandler();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseAntiforgery();

    app.MapControllers();

    logger.LogInformation("Application configured successfully, starting the app");
    app.Run();
} 
catch ( OptionsValidationException ex)
{
    startupLogger.LogCritical("Configuration validation failed: {Failures}", string.Join(", ", ex.Failures));
    throw;
}
catch (Exception ex)
{
    startupLogger.LogError(ex, "An unexpected error occurred during application startup: {Message}", ex.Message);
    throw;
}
