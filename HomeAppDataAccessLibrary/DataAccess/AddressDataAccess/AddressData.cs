using HomeAppDataAccessLibrary.DataAccess.HomeModelDataAccess;
using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.DTOs.AddressDTO;

namespace HomeAppDataAccessLibrary.DataAccess.AddressDataAccess;

public class AddressData : IAddressData
{
    private readonly ISqlDataAccess dataAccess;
    private readonly IHomeDataAccess homeData;

    public AddressData(ISqlDataAccess dataAccess, IHomeDataAccess homeData)
    {
        this.dataAccess = dataAccess;
        this.homeData = homeData;
    }

    public async Task<List<ReadAddressDTO>> GetAddressesByUserId(int id)
    {
        var output = await dataAccess.LoadData<ReadAddressDTO, dynamic>(
                storedProcedure: "dbo.spSelectAddress",
                new { Id = id },
                connectionStringName: "Default");

        if(output == null)
        {
            return null;
        }

        //foreach (var home in output)
        //{
        //    var homes = await homeData.GetHomeModelsById(home.Id);

        //    if (homes != null)
        //    {
        //        home.HomeModels.AddRange(homes);
        //    }
        //}

        return output;
    }

    public Task AddAddressByUserId(ReadAddressDTO addressDTO)
    {
        var output = dataAccess.SaveData<dynamic>(
        storedProcedure: "dbo.spInsertAddress",
        new { Country = addressDTO.Country, City = addressDTO.City, Street = addressDTO.Street, Id = addressDTO.Id },
        connectionStringName: "Default");

        return output;
    }

    public Task DeleteAddressByHomeModelId(int id)
    {
        return dataAccess.SaveData<dynamic>(
        storedProcedure: "dbo.spDeleteAddress",
        new { Id = id },
        connectionStringName: "Default");
    }

    public async Task<List<ReadAddressDTO>> UpdateUser(AddressModel address)
    {
        var result = await dataAccess.LoadData<ReadAddressDTO, dynamic>(
            storedProcedure: "dbo.spUpdateAddress",
            new { Id = address.Id, Country = address.Country, City = address.City, Street = address.Street },
            connectionStringName: "Default");

        if (result != null)
        {
            return null;
        }

        return result;
    }
}
