using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageAccessor.Models;
using ImageAccessor.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageAccessor.Controllers
{
	[Route("api/image")]
	public class ImageController : Controller
	{
		private readonly IImageService _imageService;
		public ImageController(IImageService imageService)
		{
			_imageService = imageService;
		}

		[HttpPost]
		[Route("/")]
		[Route("create")]
		public async Task<Response> Create(IFormFile file)
		{
			return await _imageService.Create(file);
		}

		[HttpGet]
		[Route("/")]
		[Route("/desc")]
		public IEnumerable<string> GetImagesByDescending()
		{
			return _imageService.GetImagesByDescending();
		}

		[HttpGet, Route("/asc")]
		public IEnumerable<string> GetImagesByAscending()
		{
			return _imageService.GetImagesByAscending();
		}

		[HttpPost, Route("/{name}/images")]
		public IEnumerable<string> GetImages(string name)
		{
			return _imageService.GetImages(name);
		}
	}
}
