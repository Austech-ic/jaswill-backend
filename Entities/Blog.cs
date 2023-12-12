using System;
using System.Collections.Generic;
using CMS_appBackend.Contracts;


namespace CMS_appBackend.Entities
{
    public class Blog: AuditableEntity
    {
        public string ImageUrl { get; set; }
        public string Title {get;set;}
        public DateTime CreatedOn {get; set;}
       public List<Post> Posts {get; set;} 
    }
}