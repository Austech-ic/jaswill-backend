using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS_appBackend.Contracts;
using CMS_appBackend.Entities;

namespace CMS_appBackend.Entities
{
    public class Category : AuditableEntity
    {
        public string CategoryName {get; set;}
        public string? Title {get; set;}
        public string? Description {get; set;}
        public string? Price {get; set;}
        public List<Blog> Blogs {get; set;}

    }
}