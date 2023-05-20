using API.Contexts;
using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BookingManagementDbContext>(options => options.UseSqlServer(connectionString));


// Add Repository to the container.
builder.Services.AddScoped<IController<University>, ControllerRepository<University>>();
builder.Services.AddScoped<IController<Education>, ControllerRepository<Education>>();
builder.Services.AddScoped<IController<Room>, ControllerRepository<Room>>();
builder.Services.AddScoped<IController<Booking>, ControllerRepository<Booking>>();
builder.Services.AddScoped<IController<Role>, ControllerRepository<Role>>();
builder.Services.AddScoped<IController<Account>, ControllerRepository<Account>>();
builder.Services.AddScoped<IController<AccountRole>, ControllerRepository<AccountRole>>();
builder.Services.AddScoped<IController<Employee>, ControllerRepository<Employee>>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
