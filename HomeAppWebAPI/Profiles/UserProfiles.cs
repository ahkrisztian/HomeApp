using AutoMapper;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.UserModels;

namespace HomeAppWebAPI.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<UpdateUserDTO, UserModel>();
            CreateMap<ReadUserDTO, UserModel>();
            CreateMap<UpdateUserDTO, ReadUserDTO>();
            CreateMap<CreateUserDTO, UserModel>();
            CreateMap<UserModel, CreateUserDTO>();
            CreateMap<UserModel, ReadUserDTO>();
        }
    }
}
