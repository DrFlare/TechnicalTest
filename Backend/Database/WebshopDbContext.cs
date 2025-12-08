using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.DataAccess;

public class WebshopDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<ProductModel> Products { get; set; }
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<ProductModel>().Property(p => p.Id).IsRequired();
	}
}