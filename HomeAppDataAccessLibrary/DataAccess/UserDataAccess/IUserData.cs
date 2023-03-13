using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.UserModels;

namespace HomeAppDataAccessLibrary.DataAccess.UserDataAccess;

public interface IUserData
{
    Task<ReadUserDTO> CreateUser(CreateUserDTO createUser);
    Task DeleteUser(int id);
    Task<UserModel> GetUserById(int id);
    Task<ReadUserDTO> GetUserByObjectId(string objectid);
    Task<ReadUserDTO> UpdateUser(UserModel userModel);

    Task<ReadUserDTO> GetUserFullByObjectId(string objectid);
}