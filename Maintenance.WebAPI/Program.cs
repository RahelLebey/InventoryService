using Maintenance.WebAPI.Services;
using Maintenance.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Controllers (needed for MaintenanceController)
builder.Services.AddControllers();

// Swagger (UI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Fake service DI
builder.Services.AddSingleton<IRepairHistoryService, FakeRepairHistoryService>();


var app = builder.Build();

// Swagger UI (enable for dev + Azure)
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

//Test endpoint to prove exception handling works
app.MapGet("/throw", () => { throw new Exception("Test exception"); });

app.MapGet("/debug/apikey", (IConfiguration config) =>
{
    var key = config["ApiSettings:ApiKey"];
    return Results.Ok(new { hasKey = !string.IsNullOrWhiteSpace(key) });
});



//Global Exception Handling Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
