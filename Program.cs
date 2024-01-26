using CMS_appBackend.Context;
using CMS_appBackend.Implementations.Repositories;
using CMS_appBackend.Implementations.Services;
using CMS_appBackend.Interface.Repositories;
using CMS_appBackend.Interface.Services;
using CMS_appBackend.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CMS_appBackend.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using CMS_appBackend.Identity;
using Microsoft.AspNetCore.Identity;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CMS_appBackend.Entities;
using Microsoft.OpenApi.Models;
using System.Reflection;
using CMS_appBackend.OperationFilters;
using CloudinaryDotNet.Actions;
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

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<DatabaseInitializer>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddControllers();


// var connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

// builder.Services.AddDbContext<ApplicationContext>(options =>
//     options.UseNpgsql(connectionString)
// );


builder.Configuration.AddEnvironmentVariables();
    

var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(connectionString)
);

// builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

var cloudinarySettings = new CloudinarySettings
{
    CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME"),
    ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY"),
    ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET")
};

builder.Services.AddSingleton(cloudinarySettings);

var key = "This is an authorization key";
builder.Services.AddSingleton<IJWTAuthentication>(new JWTAuthentication(key));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Configure Swagger to accept file uploads
    c.OperationFilter<AddFileParamTypesOperationFilter>();
    c.OperationFilter<AddFileUploadParamsOperationFilter>();

    // // Set the comments path for the Swagger JSON and UI
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});
var app = builder.Build();
// ... (other app configurations)

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseStaticFiles();
app.UseAuthentication();
app.MapHealthChecks("/_health");
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var initializer = serviceProvider.GetRequiredService<DatabaseInitializer>();
    await initializer.Initialize().ConfigureAwait(false);
}

app.Run();