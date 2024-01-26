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
        public int Id {get; set;}
        public string CategoryName {get; set;}
        public string? Description {get; set;}
        public List<RealEstate> RealEstates {get; set;}

    }
}