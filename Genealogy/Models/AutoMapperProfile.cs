using AutoMapper;
using Genealogy.Models;

namespace Genealogy.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CemeteryDto, Cemetery>();
            CreateMap<Cemetery, CemeteryDto>();

            CreateMap<PersonDto, Person>();
            CreateMap<Person, PersonDto>();
            
            CreateMap<PageDto, Page>();
            CreateMap<Page, PageDto>();
        }
    }
}