using System;


namespace CMS_appBackend.DTOs
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Desccription {get; set;}
        public string CreatedOn {get; set;}
        public string Title {get;set;}
        public List<CommentDTO> CommentDTOs {get; set;} = new List<CommentDTO>();
    }
} 