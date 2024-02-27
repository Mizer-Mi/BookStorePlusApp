using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace Api.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book,BookDto>().ReverseMap();
            CreateMap<BookDtoForUpdate, Book>().ReverseMap();
            CreateMap<BookDtoForUpdate, BookDto>().ReverseMap();
            CreateMap<BookDtoForInsertion, Book>().ReverseMap();
            CreateMap<BookDtoForInsertion, BookDto>().ReverseMap();
        }

    }
}
