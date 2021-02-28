using ImageAccessor.Entities;
using ImageAccessor.Models;
using ImageAccessor.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageAccessor.Services
{
	public class ImageService : IImageService
	{
		private readonly IImageRepository _imageRepository;
		private readonly string[] _supportedType = new string[] { "image/jpeg", "image/png", "image/bmp" };
		public ImageService(IImageRepository imageRepository)
		{
			_imageRepository = imageRepository;
		}

		private Response InvalidResponse(int code, string errorMessage)
		{
			return new Response
			{
				Error = new Error
				{
					Code = code,
					Message = errorMessage
				}
			};
		}

		public async Task<Response> Create(IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return InvalidResponse(422, "Invalid Data");
			}

			if ((file.Length / 1024f) / 1024f > 2)
			{
				return InvalidResponse(422, "File size should be less than 2MB");
			}

			if (!_supportedType.Contains(file.ContentType))
			{
				return InvalidResponse(422, "File format not supported");
			}

			var image = new Image
			{
				Id = Guid.NewGuid(),
				Name = Path.GetFileName(file.FileName),
				Type = file.ContentType,
				CreatedOn = DateTime.Now
			};
			using (var reader = new BinaryReader(file.OpenReadStream()))
			{
				image.Content = reader.ReadBytes((int)file.Length);
			}

			await _imageRepository.Add(image);
			return new Response
			{
				Value = image
			};
		}

		public IEnumerable<string> GetImagesByDescending()
		{
			return _imageRepository.GetImages()
				.OrderByDescending(x => x.CreatedOn)
				.Select(x => x.Name);
		}

		public IEnumerable<string> GetImagesByAscending()
		{
			return _imageRepository.GetImages()
				.OrderBy(x => x.CreatedOn)
				.Select(x => x.Name);
		}

		public IEnumerable<string> GetImages(string name)
		{
			return _imageRepository.GetImages(name)
				.OrderBy(x => x.CreatedOn)
				.Select(x => x.Name);
		}
	}
}
