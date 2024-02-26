using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace Api.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book,BookDto>();
            CreateMap<BookDtoForUpdate, Book>();
        }

    }
}
