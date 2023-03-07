using HomeAppDataAccessLibrary.DataAccess.AddressDataAccess;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.UserModels;

namespace HomeAppDataAccessLibrary.DataAccess.UserDataAccess;

public class UserData : IUserData
{
    private readonly ISqlDataAccess dataAccess;
    private readonly IAddressData addressData;

    public UserData(ISqlDataAccess dataAccess, IAddressData addressData)
    {
        this.dataAccess = dataAccess;
        this.addressData = addressData;
    }

    public Task<List<UserModel>> GetUserById(int id)
    {

        var output = dataAccess.LoadData<UserModel, dynamic>(
                storedProcedure: "dbo.spSelectUserById",
                new { Id = id },
                connectionStringName: "Default");

        return output;
    }

    public Task<List<ReadUserDTO>> GetUserByObjectId(int objectid)
    {
        return dataAccess.LoadData<ReadUserDTO, dynamic>(
                storedProcedure: "dbo.spSelectUserByObjectId",
                new { ObjectId = objectid },
                connectionStringName: "Default");
    }

    public Task DeleteUser(int id)
    {
        return dataAccess.SaveData<dynamic>(
            storedProcedure: "dbo.spDeleteUserById",
            new { UserId = id },
            connectionStringName: "Default");
    }

    public Task<List<ReadUserDTO>> CreateUser(CreateUserDTO createUser)
    {
        return dataAccess.LoadData<ReadUserDTO, dynamic>(
            storedProcedure: "dbo.spInsertUser",
            new { Name = createUser.Name, Email = createUser.Email, ObjectId = createUser.ObjectId },
            connectionStringName: "Default");
    }

    public async Task<List<ReadUserDTO>> UpdateUser(UserModel userModel)
    {
        var result = await dataAccess.LoadData<UserModel, dynamic>(
            storedProcedure: "dbo.spUpdateUser",
            new { Id = userModel.Id, Name = userModel.Name, Email = userModel.Email, ObjectId = userModel.ObjectId },
            connectionStringName: "Default");

        if(result != null)
        {
            return await dataAccess.LoadData<ReadUserDTO, dynamic>(
                            storedProcedure: "dbo.spSelectUserById",
                            new { Id = result.FirstOrDefault().Id },
                            connectionStringName: "Default");
        }

        return null;
        
    }

    public async Task<ReadUserDTO> GetUserFullByObjectId(int objectid)
    {
        var output = await dataAccess.LoadData<ReadUserDTO, dynamic>(
                storedProcedure: "dbo.spSelectUserByObjectId",
                new { ObjectId = objectid },
                connectionStringName: "Default");

        if(output == null)
        {
            return null;
        }

        var user = output.FirstOrDefault();

        if(user != null)
        {
            var addresses = await addressData.GetAddressesByUserId(user.Id);

            if (addresses != null)
            {
                user.Addresses.AddRange(addresses);
            }           
        }

        return user;
    }
}
