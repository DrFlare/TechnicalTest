using Backend.DataAccess;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers().Services.AddDbContext<WebshopDbContext>(options => 
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ProductService>();

if (builder.Environment.IsDevelopment())
{
	builder.Services.AddScoped<DummyDataService>(provider => new DummyDataService(provider.GetRequiredService<ProductService>()));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/dbtest", (ProductService productService) =>
{
	var products = productService.Get();
	return products;
});

app.MapGet("/insertDummyData", async (DummyDataService dummyDataService) =>
{
	if (!app.Environment.IsDevelopment()) return Results.BadRequest();
	await dummyDataService.InsertDummyData();
	return Results.Ok();
});

app.Run();