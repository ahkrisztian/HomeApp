using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;

namespace HomeAppMAUI.DataServices
{
    public interface IUserDataService
    {
        Task<ReadUserDTO> GetUserById(int id);
    }
}