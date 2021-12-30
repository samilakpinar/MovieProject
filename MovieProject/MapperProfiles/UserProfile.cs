using AutoMapper;
using Business.Models;

namespace MovieProject.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<source object, sender object>
            //Write rules
            CreateMap<User, UserInfoDto>()
                .ForMember(destination => destination.FullName, operation => operation.MapFrom(source => source.Name + " " + source.Surname)); //FullName = Name + Surname
    
        }

    }
}
