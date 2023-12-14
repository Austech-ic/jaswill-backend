using CMS_appBackend.Entities;
using CMS_appBackend.DTOs;
namespace CMS_appBackend.Interface.Repositories
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        IList<ImageDto> GetImagesByBlogId(int id);
        IList<ImageDto> GetImagesByPostId(int id);
        IList<ImageDto> GetImagesByRealEstateId(int id);
    }

}