using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageAccessor.Entities
{
	public class ImageConfiguration : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{

			builder
				.Property(i => i.Name)
				.HasMaxLength(30)
				.IsRequired(true);
		}
	}
}
