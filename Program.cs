using CMS_appBackend.Context;
using CMS_appBackend.Implementations.Repositories;
using CMS_appBackend.Implementations.Services;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;
using CMS_appBackend.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using CMS_appBackend.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(
    a =>
        a.AddPolicy(
            "CorsPolicy",
            b =>
            {
                b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            }
        )
);

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<DatabaseInitializer>();
builder.Services.AddScoped<HealthCheckMiddlewares>();
builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddControllers();

// var connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

// builder.Services.AddDbContext<ApplicationContext>(options =>
//     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
// );

builder.Configuration.AddEnvironmentVariables();
    

var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/CMS_appBackend/login";
        config.Cookie.Name = "CMS_appBackend";
        config.LogoutPath = "/CMS_appBackend/Logout";
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// var rewriteOptions = new RewriteOptions()
//     .AddRedirect("^$", "swagger");  // Redirect from root to Swagger UI

// app.UseRewriter(rewriteOptions);

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthentication();


app.MapHealthChecks("/health");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var initializer = serviceProvider.GetRequiredService<DatabaseInitializer>();
    await initializer.Initialize().ConfigureAwait(false);
}
app.Run();

// builder.Services.AddScoped<IAdminService, AdminService>();
// builder.Services.AddScoped<IAdminRepository, AdminRepository>();

// builder.Services.AddScoped<IBlogService, BlogService>();
// builder.Services.AddScoped<IBlogRepository, BlogRepository>();

// builder.Services.AddScoped<ICommentService, CommentService>();
// builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// builder.Services.AddScoped<ICategoryService, CategoryService>();
// builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// builder.Services.AddScoped<IPostService, PostService>();
// builder.Services.AddScoped<IPostRepository, PostRepository>();

// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();