using AutoMapper;

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
            CreateMap<Page, PageWithLinksDto>();

            CreateMap<Page, PageListItemDto>();

            CreateMap<Link, LinkDto>();
            CreateMap<LinkDto, Link>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<User, RegistrationUserDto>();
            CreateMap<RegistrationUserDto, User>();

            CreateMap<BusinessObject, BusinessObjectOutDto>();
            CreateMap<BusinessObjectInDto, BusinessObject>();
            CreateMap<BusinessObject, BusinessObjectInDto>();

            CreateMap<Metatype, MetatypeOutDto>();
            CreateMap<MetatypeOutDto, Metatype>();

            CreateMap<PersonGroup, PersonGroupDto>();
            CreateMap<PersonGroupDto, PersonGroup>();

            CreateMap<County, CountyDto>();
            CreateMap<CountyDto, County>();
        }
    }
}