using Bob.Core;
using Bob.Core.Services;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository;
using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using BobAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
	.WriteTo.File("log/boblogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddTransient<ExceptionMiddlewareExtension>();

//options.UseSqlServer(connection, b => b.MigrationsAssembly("BobAPI"))

builder.Services.AddDbContext<ApplicationDbContext>(options =>
				 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(ProfileConfig));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<ExceptionMiddlewareExtension>();

app.MapControllers();

app.Run();


