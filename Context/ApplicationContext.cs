using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS_appBackend.Entities;
using CMS_appBackend.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMS_appBackend.Context
{
    public class ApplicationContext : DbContext 
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options): base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles{get;set;}
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments{get;set;}
        public DbSet<Customer> Customers{get;set;}
        
        
    }
}
