using Backend.DataAccess;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers().Services.AddDbContext<WebshopDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
		sqlOptions => sqlOptions.EnableRetryOnFailure(
			maxRetryCount: 5,
			maxRetryDelay: TimeSpan.FromSeconds(10),
			errorNumbersToAdd: null)));

builder.Services.AddSingleton<InventoryService>();
builder.Services.AddHostedService<CartCleanupService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService, ProductService>();

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

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<WebshopDbContext>();

	if (!await dbContext.Database.CanConnectAsync())
	{
		await dbContext.Database.EnsureCreatedAsync();
	}

	var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
	if (pendingMigrations.Any())
	{
		await dbContext.Database.MigrateAsync();
	}
}

app.UseCors("AllowWebFrontend");

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
	app.MapGet("/loadDummyData", async (IProductService productService) =>
	{
		await productService.ResetAndLoadDummyData();
		return Results.Ok();
	});
}

app.MapControllers();

app.Run();