using ImageAccessor.Repositories;
using ImageAccessor.Services;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageAccessor.Test
{
	public class ImageAccessorTests
	{
		[Test, Order(1)]
		public async Task Create_Invalid_Image()
		{
			var imageRepository = Substitute.For<IImageRepository>();
			var imageService = new ImageService(imageRepository);
			IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
			var response = await imageService.Create(file);
			Assert.AreEqual(response.Error.Code, 422);
		}

		[Test, Order(2)]
		public async Task Create_Invalid_Image_Length()
		{
			var imageRepository = Substitute.For<IImageRepository>();
			var imageService = new ImageService(imageRepository);
			IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 3284257, "Data", "dummy.jpg");
			var response = await imageService.Create(file);
			Assert.AreEqual(response.Error.Code, 422);
		}

		[Test, Order(3)]
		public void Create_Invalid_Image_Type()
		{
			var imageRepository = Substitute.For<IImageRepository>();
			var imageService = new ImageService(imageRepository);
			IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 1000, "Data", "dummy.dox");
			Assert.ThrowsAsync<NullReferenceException>(async () => await imageService.Create(file));
		}
	}
}