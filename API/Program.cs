using API.Contexts;
using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BookingManagementDbContext>(options => options.UseSqlServer(connectionString));


// Add Repository to the container.
builder.Services.AddScoped<IUniveristyRepository, UniversityRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

//Add Singleton
builder.Services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));

//add email service to the container
builder.Services.AddTransient<IEmailService, EmailService>(_ => new EmailService(
             smtpServer: builder.Configuration["Email:SmtpServer"],
             smtpPort: int.Parse(builder.Configuration["Email:SmtpPort"]),
             fromEmailAddress: builder.Configuration["Email:FromEmailAddress"]));


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
