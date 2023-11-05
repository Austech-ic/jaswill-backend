using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS_appBackend.Contracts;
namespace CMS_appBackend.Identity;
public class Role : AuditableEntity
{
    public string Name {get; set;}
    public string Description {get; set;}
    public ICollection<UserRole> UserRoles {get; set;} =  new HashSet<UserRole>();
}