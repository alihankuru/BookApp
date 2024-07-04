using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Concrete;
using BookApp.BusinessLayer.Serilog;
using BookApp.BusinessLayer.Validators.ShelfLocationValidators;
using BookApp.DataAccessLayer.Abstract;
using BookApp.DataAccessLayer.Context;
using BookApp.DataAccessLayer.EntityFramework;
using BookApp.DataAccessLayer.UnitOfWork;
using BookApp.DtoLayer.ShelfLocation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));


builder.Services.AddDbContext<BookContext>();

builder.Services.AddScoped<IShelfLocationDal, EfShelfLocationDal>();
builder.Services.AddScoped<IShelfLocationService, ShelfLocationManager>();
builder.Services.AddScoped<IUowDal, UowDal>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.AddScoped<IValidator<CreateShelfLocationDto>, CreateShelfLocationValidator>();
builder.Services.AddScoped<IValidator<UpdateShelfLocationDto>, UpdateShelfLocationValidator>();

builder.Services.AddControllers().AddFluentValidation(x => {
    x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

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
