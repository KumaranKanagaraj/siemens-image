using ImageAccessor.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ImageAccessor.Repositories
{
	public interface IImageRepository
	{
		Task<Image> Add(Image image);
		IQueryable<Image> GetImages();
		IQueryable<Image> GetImages(string name);
	}
}