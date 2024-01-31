namespace CMS_appBackend.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public IList<RealEstateDto> RealEstates {get; set;} = new List<RealEstateDto>();
    }
}
