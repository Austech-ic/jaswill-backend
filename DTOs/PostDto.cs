using System;
using CMS_appBackend.Entities;


namespace CMS_appBackend.DTOs
{
    public class PostDto 
    {
        public string PostName {get; set;}
        public string PostImage {get; set;}
        public string Content {get; set;}
        public string PostTag {get; set;}
    }
}