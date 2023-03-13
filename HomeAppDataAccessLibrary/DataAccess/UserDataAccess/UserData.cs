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

    public async Task<UserModel> GetUserById(int id)
    {

        var output = await dataAccess.LoadData<UserModel, dynamic>(
                storedProcedure: "dbo.spSelectUserById",
                new { Id = id },
                connectionStringName: "Default");

        if(output == null)
        {
            return null;
        }

        return output.FirstOrDefault();
    }

    public async Task<ReadUserDTO> GetUserByObjectId(string objectid)
    {
        var output = await dataAccess.LoadData<ReadUserDTO, dynamic>(
                storedProcedure: "dbo.spSelectUserByObjectId",
                new { ObjectId = objectid },
                connectionStringName: "Default");

        if (output == null)
        {
            return null;
        }

        return output.FirstOrDefault();
    }

    public Task DeleteUser(int id)
    {
        return dataAccess.SaveData<dynamic>(
            storedProcedure: "dbo.spDeleteUserById",
            new { UserId = id },
            connectionStringName: "Default");
    }

    public async Task<ReadUserDTO> CreateUser(CreateUserDTO createUser)
    {
        var output = await dataAccess.LoadData<ReadUserDTO, dynamic>(
            storedProcedure: "dbo.spInsertUser",
            new { Name = createUser.Name, Email = createUser.Email, ObjectId = createUser.ObjectId },
            connectionStringName: "Default");

        if (output == null)
        {
            return null;
        }

        return output.FirstOrDefault();
    }

    public async Task<ReadUserDTO> UpdateUser(UserModel userModel)
    {
        var result = await dataAccess.LoadData<UserModel, dynamic>(
            storedProcedure: "dbo.spUpdateUser",
            new { Id = userModel.Id, Name = userModel.Name, Email = userModel.Email, ObjectId = userModel.ObjectId },
            connectionStringName: "Default");

        if(result != null)
        {
            var output =  await dataAccess.LoadData<ReadUserDTO, dynamic>(
                            storedProcedure: "dbo.spSelectUserById",
                            new { Id = result.FirstOrDefault().Id },
                            connectionStringName: "Default");

            return output.FirstOrDefault();
        }

        return null;
        
    }

    public async Task<ReadUserDTO> GetUserFullByObjectId(string objectid)
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
