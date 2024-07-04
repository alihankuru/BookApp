using AutoMapper;
using BookApp.DtoLayer.Book;
using BookApp.DtoLayer.BookNote;
using BookApp.DtoLayer.Order;
using BookApp.DtoLayer.OrderItem;
using BookApp.DtoLayer.SharedNote;
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

            CreateMap<CreateBookNoteDto, BookNote>().ReverseMap();
            CreateMap<UpdateBookNoteDto, BookNote>().ReverseMap();
            CreateMap<GetByIdBookNoteDto, BookNote>().ReverseMap();
            CreateMap<ResultBookNoteDto, BookNote>().ReverseMap();

            CreateMap<CreateOrderItemDto, OrderItem>().ReverseMap();
            CreateMap<UpdateOrderItemDto, OrderItem>().ReverseMap();
            CreateMap<GetByIdOrderItemDto, OrderItem>().ReverseMap();
            CreateMap<ResultOrderItemDto, OrderItem>().ReverseMap();

            CreateMap<CreateSharedNoteDto, SharedNote>().ReverseMap();
            CreateMap<UpdateSharedNoteDto, SharedNote>().ReverseMap();
            CreateMap<GetByIdSharedNoteDto, SharedNote>().ReverseMap();
            CreateMap<ResultSharedNoteDto, SharedNote>().ReverseMap();

        }

    }
}
