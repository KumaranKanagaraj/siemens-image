using ImageAccessor.DbContexts;
using ImageAccessor.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImageAccessor.Repositories
{
	public class ImageRepository : IImageRepository
	{
		private readonly ImageAccessorContext _context;

		public ImageRepository(ImageAccessorContext context)
		{
			_context = context;
		}
		public async Task<Image> Add(Image image)
		{
			_context.Add(image);
			await _context.SaveChangesAsync();
			return image;
		}

		public IQueryable<Image> GetImages()
		{
			return _context.Images;
		}

		public IQueryable<Image> GetImages(string name)
		{
			return _context.Images.Where(x => x.Name.ToLower().Contains(name.ToLower()));
		}
	}
}
