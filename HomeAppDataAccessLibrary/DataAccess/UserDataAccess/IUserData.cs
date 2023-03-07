using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.UserModels;

namespace HomeAppDataAccessLibrary.DataAccess.UserDataAccess;

public interface IUserData
{
    Task<List<ReadUserDTO>> CreateUser(CreateUserDTO createUser);
    Task DeleteUser(int id);
    Task<List<UserModel>> GetUserById(int id);
    Task<List<ReadUserDTO>> GetUserByObjectId(int objectid);
    Task<List<ReadUserDTO>> UpdateUser(UserModel userModel);

    Task<ReadUserDTO> GetUserFullByObjectId(int objectid);
}