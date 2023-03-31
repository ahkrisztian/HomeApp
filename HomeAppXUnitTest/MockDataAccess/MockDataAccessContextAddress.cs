using AutoMapper;
using HomeAppDataAccessLibrary.DataAccess.AddressDataAccess;
using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.DTOs.AddressDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppDataAccessLibrary.Models.RoomModels;
using HomeAppDataAccessLibrary.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppXUnitTest.MockDataAccess
{
    public class MockDataAccessContextAddress : IAddressData
    {
        private readonly IMapper mapper;
        public List<UserModel> users = new List<UserModel>();
        public MockDataAccessContextAddress(IMapper mapper)
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

        public Task AddAddressByUserId(ReadAddressDTO addressDTO)
        {
            return Task.Run(() =>
            {
                users.Where(i => i.Id == 1).FirstOrDefault().Addresses.Add(mapper.Map<AddressModel>(addressDTO));

                var x = users.Where(i => i.Id == 1).FirstOrDefault();

            });
        }

        public Task DeleteAddressByHomeModelId(int id)
        {
            return Task.Run(() =>
            {
                var user = users.Where(i => i.Id == id).FirstOrDefault();

                var address = user.Addresses.Where(u => u.Id == id).FirstOrDefault();

                var index = users.Where(i => i.Id == id).FirstOrDefault().Addresses.IndexOf(address);

                users.Where(i => i.Id == id).FirstOrDefault().Addresses.RemoveAt(index);

            });
        }

        public Task<List<ReadAddressDTO>> GetAddressesByUserId(int id)
        {
            return Task.Run(() =>
            {
                var output = users.Where(i => i.Id == id).FirstOrDefault().Addresses.ToList();

                return mapper.Map<List<ReadAddressDTO>>(output);
            });
        }

        public Task<List<ReadAddressDTO>> UpdateUser(AddressModel address)
        {
            return Task.Run(() =>
            {
                int index = users.Where(i => i.Id == address.Id).FirstOrDefault().Addresses.FindIndex(i => i.Id == address.Id);

                users.Where(i => i.Id == address.Id).FirstOrDefault().Addresses[index] = address;

                var result = users.Where(i => i.Id == address.Id).FirstOrDefault().Addresses.Where(i => i.Id == address.Id);

                return mapper.Map<List<ReadAddressDTO>>(result);
            });
        }
    }
}
