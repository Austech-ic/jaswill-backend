using CMS_appBackend.Context;
using CMS_appBackend.Implementations.Repositories;
using CMS_appBackend.Implementations.Services;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;
using CMS_appBackend.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using CMS_appBackend.Identity;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var rewriteOptions = new RewriteOptions()
    .AddRedirect("^$", "swagger");  // Redirect from root to Swagger UI

app.UseRewriter(rewriteOptions);

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var initializer = serviceProvider.GetRequiredService<DatabaseInitializer>();
    await initializer.Initialize().ConfigureAwait(false);
}

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddCors(
        a =>
            a.AddPolicy(
                "CorsPolicy",
                b =>
                {
                    b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                }
            )
    );

    services.AddScoped<IAdminService, AdminService>();
    services.AddScoped<IAdminRepository, AdminRepository>();

    services.AddScoped<IBlogService, BlogService>();
    services.AddScoped<IBlogRepository, BlogRepository>();

    services.AddScoped<ICommentService, CommentService>();
    services.AddScoped<ICommentRepository, CommentRepository>();

    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();

    services.AddScoped<ICustomerRepository, CustomerRepository>();
    services.AddScoped<ICustomerService, CustomerService>();

    services.AddScoped<IPostService, PostService>();
    services.AddScoped<IPostRepository, PostRepository>();

    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IUserRepository, UserRepository>();

    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<IRoleRepository, RoleRepository>();

    services.AddScoped<IEmailSender, EmailSender>();

    services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    services.AddScoped<DatabaseInitializer>();
    services.AddHttpContextAccessor();

    // Add services to the container.
    services.AddControllers();

    var connectionString = configuration.GetConnectionString("ApplicationContext");
    services.AddDbContext<ApplicationContext>(option => option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

        // Set the schemes to include "https"
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            // Your security scheme configuration...
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            // Your security requirements...
        });

        c.Schemes = new List<OpenApiSchema> { new OpenApiSchema { Type = "https" }, new OpenApiSchema { Type = "http" } };
    });

    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(config =>
        {
            config.LoginPath = "/CMS_appBackend/login";
            config.Cookie.Name = "CMS_appBackend";
            config.LogoutPath = "/CMS_appBackend/Logout";
        });
}
