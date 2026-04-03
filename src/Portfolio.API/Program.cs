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

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// PortfolioConfig with validation extra against ""
builder.Services.AddOptions<PortfolioConfig>()
    .Bind(builder.Configuration.GetSection("PortfolioConfig"))
    .ValidateDataAnnotations()
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

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
    options.SuppressXFrameOptionsHeader = false;
});

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var config =scope.ServiceProvider.GetRequiredService<IOptions<PortfolioConfig>>().Value;

    // TODO: Change it to migration
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();

    if (!db.WebsiteConfig.Any())
    {
        var websiteConfig = new WebsiteConfig();
        websiteConfig.ChangeUserName(config.AdminUserName);
        websiteConfig.ChangeEmail(config.AdminEmail);
        var passwordHasher = new PasswordHasher<WebsiteConfig>();
        websiteConfig.UpdatePasswordHash(passwordHasher.HashPassword(websiteConfig, config.AdminPassword));

        await db.WebsiteConfig.AddAsync(websiteConfig);
        await db.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Portfolio API V1");
    });
}
app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapControllers();

app.Run();
