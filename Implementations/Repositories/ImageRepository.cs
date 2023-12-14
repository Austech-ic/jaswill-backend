using Microsoft.EntityFrameworkCore;
using CMS_appBackend.Context;
using CMS_appBackend.Entities;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.DTOs;

namespace CMS_appBackend.Implementations.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationContext Context)
        {
            _Context = Context;
        }
        
        public IList<ImageDto> GetImagesByBlogId(int id)
        {
           var imgs = _Context.Images.Where(x => x.blogId == id).Select(d => new ImageDto
           {
               Name = d.Path
           }).ToList();
           return imgs;
        }

        public IList<ImageDto> GetImagesByPostId(int id)
        {
           var imgs = _Context.Images.Where(x => x.postId == id).Select(d => new ImageDto
           {
               Name = d.Path
           }).ToList();
           return imgs;
        }

        public IList<ImageDto> GetImagesByRealEstateId(int id)
        {
           var imgs = _Context.Images.Where(x => x.realEstateId == id).Select(d => new ImageDto
           {
               Name = d.Path
           }).ToList();
           return imgs;
        }
    }
}