using ImageAccessor.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageAccessor.Services
{
	public interface IImageService
	{
		Task<Response> Create(IFormFile file);
		IEnumerable<string> GetImages(string name);
		IEnumerable<string> GetImagesByAscending();
		IEnumerable<string> GetImagesByDescending();
	}
}