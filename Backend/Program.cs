using Backend.DataAccess;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers().Services.AddDbContext<WebshopDbContext>(options => 
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ProductService>();

if (builder.Environment.IsDevelopment())
{
	builder.Services.AddSingleton<DummyDataService>();
}

var app = builder.Build();

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