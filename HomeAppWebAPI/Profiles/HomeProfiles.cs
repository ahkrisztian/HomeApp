using AutoMapper;
using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.DTOs.AddressDTO;
using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppDataAccessLibrary.Models.UserModels;

namespace HomeAppWebAPI.Profiles
{
    public class HomeProfiles : Profile
    {
        public HomeProfiles()
        {
            CreateMap<UpdateHomeModelDTO, ReadHomeModelDTO>();
            CreateMap<HomeModel, CreateHomeModelDTO>();
            CreateMap<HomeModel, ReadHomeModelDTO>();
            CreateMap<CreateHomeModelDTO, HomeModel>();

            CreateMap<UpdateUserDTO, UserModel>();
            CreateMap<ReadUserDTO, UserModel>();
            CreateMap<UpdateUserDTO, ReadUserDTO>();
            CreateMap<CreateUserDTO, UserModel>();
            CreateMap<UserModel, CreateUserDTO>();
            CreateMap<UserModel, ReadUserDTO>();

            CreateMap<AddressModel, ReadAddressDTO>();
            CreateMap<ReadAddressDTO, AddressModel>();
        }
    }
}
