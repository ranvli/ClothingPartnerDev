using ClothingPartnerAPI.Services.Contracts;
using ClothingPartnerAPI.Services;
using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.DAL;
using ClothingPartnerAPI.DAL.Context;
using AutoMapper;
using ClothingPartnerAPI.DTO;
using ClothingPartnerAPI.Models;
using ClothingPartnerAPI.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register db context
builder.Services.AddDbContext<ClothingPartnerContext>();


// Configurar AutoMapper
var mapperConfig = new MapperConfiguration(cfg => {
    cfg.AddProfile<MappingProfile>();
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


//register services & repositories
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
    //}

    app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
