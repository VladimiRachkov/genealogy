using AutoMapper;
using Genealogy.Models;
using Genealogy.Models.Domain.Dtos;

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
        }
    }
}