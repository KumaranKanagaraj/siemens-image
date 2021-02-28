using ImageAccessor.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageAccessor.DbContexts
{
	public class ImageAccessorContext : DbContext
	{
		public ImageAccessorContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Image> Images { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ImageConfiguration());
		}
	}
}
