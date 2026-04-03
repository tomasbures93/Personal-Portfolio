using Microsoft.EntityFrameworkCore;
using Portfolio.API.Exceptions;
using Portfolio.Application.DependencyInjection;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.DependencyInjection;
using Portfolio.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Database Config
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration["ConnectionString"]));

// Dependency Injections
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// GlobalExceptionHandler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // TODO: Change it to migration
    await db.Database.EnsureCreatedAsync();

    if (!db.WebsiteConfig.Any())
    {
        // FAKE DATA FOR TESTING! WILL BE CHANGED SOON!
        var config = new WebsiteConfig();
        config.ChangeUserName("TestUser");
        config.ChangeEmail("email");
        config.UpdatePassword("password");

        await db.WebsiteConfig.AddAsync(config);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
