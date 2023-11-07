using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS_appBackend.Entities;
using CMS_appBackend.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMS_appBackend.Context
{
    public class ApplicationContext : DbContext 
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options): base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Use environment variables to set the connection string
            string host = Environment.GetEnvironmentVariable("Render_PostgreSQL_Host");
            string port = Environment.GetEnvironmentVariable("Render_PostgreSQL_Port");
            string database = Environment.GetEnvironmentVariable("Render_PostgreSQL_Database");
            string username = Environment.GetEnvironmentVariable("Render_PostgreSQL_Username");
            string password = Environment.GetEnvironmentVariable("Render_PostgreSQL_Password");

            string connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";

            optionsBuilder.UseNpgsql(connectionString);
        }
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
