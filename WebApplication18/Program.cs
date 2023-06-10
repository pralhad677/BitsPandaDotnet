using Repository;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Service;
using IService;
using IRepository;
using AutoMapper;
using MyMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("connectionString");
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

//builder.Services.AddDbContext<MyDbContext>(options =>
//{
//    options.UseNpgsql(connectionString); 
//});

var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile<MyMappingProfile>();
    // Add more profiles as needed
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped(typeof(IService<>),typeof(Service<>));
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped(typeof(IAdminService<>),typeof(AdminService<>));
builder.Services.AddScoped(typeof(IAdminRepo<>),typeof(AdminRepo<>));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey  = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
