using Backend.DataAccess;
using Backend.Models;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers().Services.AddDbContext<WebshopDbContext>(options => 
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<InventoryService>();
builder.Services.AddHostedService<CartCleanupService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<ProductService>();

if (builder.Environment.IsDevelopment())
{
	builder.Services.AddSingleton<DummyDataService>();
}

var corsSettings = builder.Configuration.GetSection("Cors").Get<CorsSettings>();

if (corsSettings is { AllowedOrigins.Length: > 0 })
{
	builder.Services.AddCors(options =>
	{
		options.AddPolicy("AllowWebFrontend",
			policy =>
			{
				policy.WithOrigins(corsSettings.AllowedOrigins)
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials();
			});
	});
}

var app = builder.Build();

app.UseCors("AllowWebFrontend");

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
	app.MapGet("/loadDummyData", async (ProductService productService) =>
	{
		await productService.ResetAndLoadDummyData();
		return Results.Ok();
	});
}

app.MapControllers();

app.Run();