var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HttpClientFactory for Maintenance Web API (includes API Key header)
builder.Services.AddHttpClient("MaintenanceApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MaintenanceApi:BaseUrl"]!);
    client.DefaultRequestHeaders.Add("X-Api-Key", builder.Configuration["MaintenanceApi:ApiKey"]!);
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

//Default route goes directly to Vehicle History
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Maintenance}/{action=History}/{id?}")
    .WithStaticAssets();

app.Run();
