using AutoMapper;
using BookApp.DtoLayer.Book;
using BookApp.DtoLayer.Order;
using BookApp.DtoLayer.ShelfLocation;
using BookApp.EntityLayer.Concrete;

namespace BookApp.WebApi.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreateShelfLocationDto, ShelfLocation>().ReverseMap();
            CreateMap<GetByIdShelfLocationDto, ShelfLocation>().ReverseMap();
            CreateMap<ResultShelfLocationDto, ShelfLocation>().ReverseMap();
            CreateMap<UpdateShelfLocationDto, ShelfLocation>().ReverseMap();

            CreateMap<CreateBookDto, Book>().ReverseMap();
            CreateMap<UpdateBookDto, Book>().ReverseMap();
            CreateMap<GetByIdBookDto, Book>().ReverseMap();
            CreateMap<ResultBookDto, Book>().ReverseMap();


            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<UpdateOrderDto, Order>().ReverseMap();
            CreateMap<GetByIdOrderDto, Order>().ReverseMap();
            CreateMap<ResultOrderDto, Order>().ReverseMap();

        }

    }
}
