using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Concrete;
using BookApp.DataAccessLayer.Abstract;
using BookApp.DataAccessLayer.Context;
using BookApp.DataAccessLayer.EntityFramework;
using BookApp.DataAccessLayer.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BookContext>();

builder.Services.AddScoped<IShelfLocationDal, EfShelfLocationDal>();
builder.Services.AddScoped<IShelfLocationService, ShelfLocationManager>();
builder.Services.AddScoped<IUowDal, UowDal>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
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
