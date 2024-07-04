using AutoMapper;
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
        }

    }
}
