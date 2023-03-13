using AutoMapper;
using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppDataAccessLibrary.Models.RoomModels;
using HomeAppWebAPI.Profiles;
using HomeAppXUnitTest.MockDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppXUnitTest
{
    public class HomeLogicTest
    {
        HomeProfiles realprofile;
        MapperConfiguration configuration;
        IMapper mapper;

        public HomeLogicTest()
        {
            realprofile = new HomeProfiles();
            configuration = new MapperConfiguration(config =>
            config.AddProfile(realprofile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mapper = null;
            configuration = null;
            realprofile = null;
        }

        [Fact]
        public void CreateHomeModelTest()
        {
            MockDataAccessContextHomeModel da = new MockDataAccessContextHomeModel(mapper);

            var output = da.CreateHomeModel(new CreateHomeModelDTO()
            {
                AddressId = 2,
                UserId = 3,
                Name = "New Home",
                Description = "New Home 1",
                BathRooms = new List<BathRoomModel>() { new BathRoomModel() { Id = 2, HomeModelId = 2, Description = "New Bathroom1", BathTap = true, Lights = true, RoomTypeId = 10, Shower = true, Tap = true, Windows = true, Toilet = true } },
                Rooms = new List<RoomModel>() { new RoomModel() { Id = 2, HomeModelId = 2, Description = "New Living room", RoomTypeId = 1, Electronics = true, Lights = true, Windows = true } },
                Kitchen = new List<KitchenModel> { new KitchenModel() { Id = 2, HomeModelId = 2, Description = "New Kitchen", RoomTypeId = 8, Fridge = true, Lights = true, OtherElectronics = true, Oven = true, Sink = true, Windows = true } },
                Toilet = new List<ToiletModel>() { new ToiletModel() { Id = 2, HomeModelId = 2, RoomTypeId = 9, Description = "New Toilet", Tap = true, Lights = true, Toilet = true, Windows = true } }
            }).Result;

            string expected = "New Home";
            string actual = da.homeModels.Where(n => n.Name == "New Home").FirstOrDefault().Name;

            Assert.Equal(expected, actual);

            int expectedCount = 3;
            int actualCount = da.homeModels.Count();

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void DeleteHomeTest()
        {
            MockDataAccessContextHomeModel da = new MockDataAccessContextHomeModel(mapper);

            da.DeleteHomeModel(1).Wait();

            int exepected = 1;
            int actual = da.homeModels.Count();

            Assert.Equal(exepected, actual);
        }

        [Fact]
        public void GetHomeModelsByIdTest()
        {
            MockDataAccessContextHomeModel da = new MockDataAccessContextHomeModel(mapper);

            var output = da.GetHomeModelsById(1).Result;

            int expected = 1;

            int actual = output.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpdateHomeModelTest()
        {
            MockDataAccessContextHomeModel da = new MockDataAccessContextHomeModel(mapper);

            var output = da.UpdateHomeModel(new HomeModel()
            {
                Id = 1,
                AddressId = 2,
                UserId = 3,
                Name = "Updated Home",
                Description = "Updated Home 1",
                BathRooms = new List<BathRoomModel>() { new BathRoomModel() { Id = 2, HomeModelId = 2, Description = "Updated Bathroom1", BathTap = true, Lights = true, RoomTypeId = 10, Shower = true, Tap = true, Windows = true, Toilet = true } },
                Rooms = new List<RoomModel>() { new RoomModel() { Id = 2, HomeModelId = 2, Description = "Updated Living room", RoomTypeId = 1, Electronics = true, Lights = true, Windows = true } },
                Kitchen = new List<KitchenModel> { new KitchenModel() { Id = 2, HomeModelId = 2, Description = "Updated Kitchen", RoomTypeId = 8, Fridge = true, Lights = true, OtherElectronics = true, Oven = true, Sink = true, Windows = true } },
                Toilet = new List<ToiletModel>() { new ToiletModel() { Id = 2, HomeModelId = 2, RoomTypeId = 9, Description = "Updated Toilet", Tap = true, Lights = true, Toilet = true, Windows = true } }
            }).Result;

            string expected = "Updated Home";
            string actucal = da.homeModels.Where(i => i.Id == 1).FirstOrDefault().Name;

            Assert.Equal(expected, actucal);
        }
    }
}
