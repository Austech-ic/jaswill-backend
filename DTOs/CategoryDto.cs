namespace CMS_appBackend.DTOs
{
    public class CategoryDto
    {
        public int Id {get; set;}
       public string CategoryName {get; set;}
       public string Title {get; set;}
        public string Description {get; set;}
        public string Price {get; set;}
       public List<BlogDto> Blogs {get; set;} = new List<BlogDto>();
    }
}