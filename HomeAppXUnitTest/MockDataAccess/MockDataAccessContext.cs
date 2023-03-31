using AutoMapper;
using HomeAppDataAccessLibrary.DataAccess.UserDataAccess;
using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppDataAccessLibrary.Models.RoomModels;
using HomeAppDataAccessLibrary.Models.UserModels;

namespace HomeAppXUnitTest.MockDataAccess;

public class MockDataAccessContext : IUserData
{
    private readonly IMapper mapper;
    public List<UserModel> users = new List<UserModel>();
    public MockDataAccessContext(IMapper mapper)
    {
        users.Add(new UserModel()
        {
            Id = 1,
            Name = "Tom Jones",
            Email = "jones@tom.com",
            ObjectId = "1",
            Addresses = new List<AddressModel>() { new AddressModel() { Id = 1, Country = "Germany", City = "Berlin", Street = "Tut Str. 3" } },
            HomeModels = new List<HomeModel>(){ new HomeModel() { Id = 1, UserId = 1, Name = "Jones Home", Description = "Jones Home 1",
                    BathRooms = new List<BathRoomModel>(){ new BathRoomModel(){Id = 1, HomeModelId = 1, Description = "Jones Bathroom1", BathTap = true, Lights = true, RoomTypeId = 10, Shower = true, Tap = true, Windows = true, Toilet = true}},
                    Rooms = new List<RoomModel>(){ new RoomModel() { Id = 1, HomeModelId = 1, Description = "Jones Living room", RoomTypeId = 1, Electronics = true, Lights = true, Windows = true }},
                    Kitchen = new List<KitchenModel>{ new KitchenModel() { Id = 1, HomeModelId = 1, Description = "Jones Kitchen", RoomTypeId = 8 , Fridge = true, Lights = true, OtherElectronics = true, Oven = true, Sink = true, Windows = true}},
                    Toilet = new List<ToiletModel>(){new ToiletModel() { Id= 1, HomeModelId = 1, RoomTypeId = 9, Description = "Jones Toilet", Tap = true, Lights = true, Toilet = true, Windows = true}} } }
        });

        users.Add(new UserModel()
        {
            Id = 2,
            Name = "Emma Little",
            Email = "emma@little.com",
            ObjectId = "2",
            Addresses = new List<AddressModel>() { new AddressModel() { Id = 2, Country = "Germany", City = "Hamburg", Street = "Burg Str. 77" } },
            HomeModels = new List<HomeModel>(){ new HomeModel() { Id = 2, UserId = 2, Name = "Little Home", Description = "Little Home 1",
                    BathRooms = new List<BathRoomModel>(){ new BathRoomModel(){Id = 2, HomeModelId = 2, Description = "Little Bathroom1", BathTap = true, Lights = true, RoomTypeId = 10, Shower = true, Tap = true, Windows = true, Toilet = true}},
                    Rooms = new List<RoomModel>(){ new RoomModel() { Id = 2, HomeModelId = 2, Description = "Little Living room", RoomTypeId = 1, Electronics = true, Lights = true, Windows = true }},
                    Kitchen = new List<KitchenModel>{ new KitchenModel() { Id = 2, HomeModelId = 2, Description = "Little Kitchen", RoomTypeId = 8 , Fridge = true, Lights = true, OtherElectronics = true, Oven = true, Sink = true, Windows = true}},
                    Toilet = new List<ToiletModel>(){new ToiletModel() { Id= 2, HomeModelId = 2, RoomTypeId = 9, Description = "Little Toilet", Tap = true, Lights = true, Toilet = true, Windows = true}} } }
        });
        this.mapper = mapper;
    }

    

    public Task<ReadUserDTO> CreateUser(CreateUserDTO createUser)
    {
        return Task.Run(() =>
        {
            users.Add(mapper.Map<UserModel>(createUser));

            var user = users.Where(n => n.Name == "Ed Williams").FirstOrDefault();

            return mapper.Map<ReadUserDTO>(user);
        });
    }

    

    public Task DeleteUser(int id)
    {
        return Task.Run(() =>
        {
            var user = users.Where(i => i.Id == id).FirstOrDefault();

            var index = users.IndexOf(user);

            users.RemoveAt(index);

        });           
    }

    

    public Task<UserModel?> GetUserById(int id)
    {
        return Task.Run(() =>
        {
            return users.Where(i => i.Id == id).FirstOrDefault();
        }); 
    }

    public Task<ReadUserDTO> GetUserByObjectId(string objectid)
    {
        return Task.Run(() =>
        {
            var output = users.Where(i => i.ObjectId == objectid).FirstOrDefault();

            return mapper.Map<ReadUserDTO>(output);
        });
    }

    public Task<ReadUserDTO> GetUserFullByObjectId(string objectid)
    {
        return Task.Run(() =>
        {
            var output = users.Where(i => i.ObjectId == objectid).FirstOrDefault();

            return mapper.Map<ReadUserDTO>(output);
        });
    }

    public Task<ReadUserDTO> UpdateUser(UserModel userModel)
    {
        return Task.Run(() =>
        {              
            int index = users.FindIndex(i => i.Id == userModel.Id);

            users[index] = userModel;

            var updatedUser = mapper.Map<ReadUserDTO>(users.Where(i => i.Id == userModel.Id).FirstOrDefault());

            return updatedUser;
        });
    }

    
}
