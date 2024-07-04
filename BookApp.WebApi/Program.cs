using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Concrete;
using BookApp.BusinessLayer.Serilog;
using BookApp.BusinessLayer.Validators.BookNoteValidators;
using BookApp.BusinessLayer.Validators.BookValidators;
using BookApp.BusinessLayer.Validators.OrderItemValidators;
using BookApp.BusinessLayer.Validators.OrderValidators;
using BookApp.BusinessLayer.Validators.SharedNoteValidators;
using BookApp.BusinessLayer.Validators.ShelfLocationValidators;
using BookApp.DataAccessLayer.Abstract;
using BookApp.DataAccessLayer.Context;
using BookApp.DataAccessLayer.EntityFramework;
using BookApp.DataAccessLayer.UnitOfWork;
using BookApp.DtoLayer.Book;
using BookApp.DtoLayer.BookNote;
using BookApp.DtoLayer.Order;
using BookApp.DtoLayer.OrderItem;
using BookApp.DtoLayer.SharedNote;
using BookApp.DtoLayer.ShelfLocation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using SharedNoteApp.BusinessLayer.Concrete;
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

builder.Services.AddScoped<IBookDal, EfBookDal>();
builder.Services.AddScoped<IBookService, BookManager>();

builder.Services.AddScoped<IOrderDal, EfOrderDal>();
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddScoped<IBookNoteDal, EfBookNoteDal>();
builder.Services.AddScoped<IBookNoteService, BookNoteManager>();

builder.Services.AddScoped<IOrderItemDal, EfOrderItemDal>();
builder.Services.AddScoped<IOrderItemService, OrderItemManager>();

builder.Services.AddScoped<ISharedNoteDal, EfSharedNoteDal>();
builder.Services.AddScoped<ISharedNoteService, SharedNoteManager>();

builder.Services.AddScoped<IUowDal, UowDal>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.AddScoped<IValidator<CreateShelfLocationDto>, CreateShelfLocationValidator>();
builder.Services.AddScoped<IValidator<UpdateShelfLocationDto>, UpdateShelfLocationValidator>();

builder.Services.AddScoped<IValidator<CreateBookDto>, CreateBookValidator>();
builder.Services.AddScoped<IValidator<UpdateBookDto>, UpdateBookValidator>();

builder.Services.AddScoped<IValidator<CreateOrderDto>, CreateOrderValidator>();
builder.Services.AddScoped<IValidator<UpdateOrderDto>, UpdateOrderValidator>();

builder.Services.AddScoped<IValidator<CreateBookNoteDto>, CreateBookNoteValidator>();
builder.Services.AddScoped<IValidator<UpdateBookNoteDto>, UpdateBookNoteValidator>();

builder.Services.AddScoped<IValidator<CreateOrderItemDto>, CreateOrderItemValidator>();
builder.Services.AddScoped<IValidator<UpdateOrderItemDto>, UpdateOrderItemValidator>();

builder.Services.AddScoped<IValidator<CreateSharedNoteDto>, CreateSharedNoteValidator>();
builder.Services.AddScoped<IValidator<UpdateSharedNoteDto>, UpdateSharedNoteValidator>();

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
