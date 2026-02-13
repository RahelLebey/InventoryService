using Maintenance.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers (needed for your MaintenanceController)
builder.Services.AddControllers();

// Swagger (UI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Your fake service DI
builder.Services.AddScoped<IRepairHistoryService, FakeRepairHistoryService>();

var app = builder.Build();

// Swagger UI (enable for dev + Azure)
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger/index.html"));




// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
