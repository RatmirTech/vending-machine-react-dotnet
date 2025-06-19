using Intravision.Vending.API.Hubs;
using Intravision.Vending.Core;
using Intravision.Vending.DAL;
using Intravision.Vending.DAL.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// builder.Configuration
//     .SetBasePath(AppContext.BaseDirectory)
//     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
//     .AddEnvironmentVariables();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var origins = Environment.GetEnvironmentVariable("CORS_ORIGINS")?
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//    {
//        policy
//            .WithOrigins(origins ?? Array.Empty<string>())
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddCore();
builder.Services.AddSignalR();

builder.Services.AddRepositories(builder.Configuration.GetConnectionString("EfContext"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EfContext>();
    dbContext.Database.Migrate();
}

app.UseCors();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapHub<VendingHub>("/vendinghub");

app.MapControllers();
app.Run();