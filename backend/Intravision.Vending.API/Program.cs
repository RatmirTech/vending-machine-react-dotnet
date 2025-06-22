using Intravision.Vending.API.Hubs;
using Intravision.Vending.Core;
using Intravision.Vending.DAL;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder.Configuration
    .AddEnvironmentVariables();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var origins = Environment.GetEnvironmentVariable("CORS_ORIGINS")?
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(origins ?? new string[] { })
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddCore();
builder.Services.AddSignalR();

var con = builder.Configuration["ConnectionStrings:EfContext"];

builder.Services.AddRepositories(con);

var app = builder.Build();

app.UseCors();

app.UseStaticFiles();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapHub<VendingHub>("/vendinghub");

app.MapControllers();
app.Run();