using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.DTOs.AddressDTO;

namespace HomeAppDataAccessLibrary.DataAccess.AddressDataAccess;

public interface IAddressData
{
    Task AddAddressByUserId(ReadAddressDTO addressDTO);
    Task DeleteAddressByHomeModelId(int id);
    Task<List<ReadAddressDTO>> GetAddressesByUserId(int id);
    Task<List<ReadAddressDTO>> UpdateUser(AddressModel address);
}