using AutoMapper;
using HomeAppDataAccessLibrary.DataAccess.HomeModelDataAccess;
using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppDataAccessLibrary.Models.RoomModels;

namespace HomeAppXUnitTest.MockDataAccess
{
    public class MockDataAccessContextHomeModel : IHomeDataAccess
    {
        private readonly IMapper mapper;

        public List<HomeModel> homeModels = new List<HomeModel>();

        public MockDataAccessContextHomeModel(IMapper mapper)
        {
            this.mapper = mapper;

            homeModels.Add(
            new HomeModel()
            {
                Id = 1,
                AddressId = 1,
                UserId = 1,
                Name = "Jones Home",
                Description = "Jones Home 1",
                BathRooms = new List<BathRoomModel>() { new BathRoomModel() { Id = 1, HomeModelId = 1, Description = "Jones Bathroom1", BathTap = true, Lights = true, RoomTypeId = 10, Shower = true, Tap = true, Windows = true, Toilet = true } },
                Rooms = new List<RoomModel>() { new RoomModel() { Id = 1, HomeModelId = 1, Description = "Jones Living room", RoomTypeId = 1, Electronics = true, Lights = true, Windows = true } },
                Kitchen = new List<KitchenModel> { new KitchenModel() { Id = 1, HomeModelId = 1, Description = "Jones Kitchen", RoomTypeId = 8, Fridge = true, Lights = true, OtherElectronics = true, Oven = true, Sink = true, Windows = true } },
                Toilet = new List<ToiletModel>() { new ToiletModel() { Id = 1, HomeModelId = 1, RoomTypeId = 9, Description = "Jones Toilet", Tap = true, Lights = true, Toilet = true, Windows = true } }
            });

            homeModels.Add(new HomeModel()
            {
                Id = 2,
                AddressId = 2,
                UserId = 2,
                Name = "Little Home",
                Description = "Little Home 1",
                BathRooms = new List<BathRoomModel>() { new BathRoomModel() { Id = 2, HomeModelId = 2, Description = "Little Bathroom1", BathTap = true, Lights = true, RoomTypeId = 10, Shower = true, Tap = true, Windows = true, Toilet = true } },
                Rooms = new List<RoomModel>() { new RoomModel() { Id = 2, HomeModelId = 2, Description = "Little Living room", RoomTypeId = 1, Electronics = true, Lights = true, Windows = true } },
                Kitchen = new List<KitchenModel> { new KitchenModel() { Id = 2, HomeModelId = 2, Description = "Little Kitchen", RoomTypeId = 8, Fridge = true, Lights = true, OtherElectronics = true, Oven = true, Sink = true, Windows = true } },
                Toilet = new List<ToiletModel>() { new ToiletModel() { Id = 2, HomeModelId = 2, RoomTypeId = 9, Description = "Little Toilet", Tap = true, Lights = true, Toilet = true, Windows = true } }
            });
        }
        public Task<HomeModel> CreateHomeModel(CreateHomeModelDTO createModel)
        {
            return Task.Run(() =>
            {
                homeModels.Add(mapper.Map<HomeModel>(createModel));

                return homeModels.Last();
            });
        }

        public Task DeleteHomeModel(int id)
        {
            return Task.Run(() =>
            {
                var homemodel = homeModels.Where(i => i.Id == id).FirstOrDefault();

                int index = homeModels.IndexOf(homemodel);

                homeModels.RemoveAt(index);
            });
        }

        public Task<List<HomeModel>> GetHomeModelsById(int id)
        {
            return Task.Run(() =>
            {
                return homeModels.Where(i => i.Id == id).ToList();
            });
        }

        public Task<HomeModel?> UpdateHomeModel(HomeModel model)
        {
            return Task.Run(() =>
            {
                var output = homeModels.Where(i => i.Id == model.Id).FirstOrDefault();

                int index = homeModels.IndexOf(output);

                homeModels[index] = model;

                return homeModels.Where(i => i.Id == model.Id).FirstOrDefault();
            });
        }
    }
}
