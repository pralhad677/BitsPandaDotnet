using Repository;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Service;
using IService;
using IRepository;
using AutoMapper;
using MyMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("connectionString");
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(connectionString); 
});

var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile<MyMappingProfile>();
    // Add more profiles as needed
});

builder.Services.AddScoped(typeof(IService<>),typeof(Service<>));
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
