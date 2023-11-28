using ClothingPartnerAPI.Services.Contracts;
using ClothingPartnerAPI.Services;
using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.DAL;
using ClothingPartnerAPI.DAL.Context;
using AutoMapper;
using ClothingPartnerAPI.Models;
using ClothingPartnerAPI.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register db context
builder.Services.AddDbContext<ClothingPartnerContext>();


// Configure AutoMapper
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
builder.Services.AddScoped<IDesignationService, DesignationService>();
builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

builder.Services.AddTransient<IEntityTypeConfiguration<Employee>, EmployeeSeed>();
builder.Services.AddIdentity<Employee, IdentityRole>()
    .AddEntityFrameworkStores<ClothingPartnerContext>()
    .AddDefaultTokenProviders();



var app = builder.Build();

// initial seeding
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var employeeSeed = serviceProvider.GetRequiredService<IEntityTypeConfiguration<Employee>>();
    employeeSeed.Configure(null);
}


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
