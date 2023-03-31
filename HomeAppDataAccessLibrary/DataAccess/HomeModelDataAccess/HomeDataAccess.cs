using HomeAppDataAccessLibrary.Models;
using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppDataAccessLibrary.Models.RoomModels;

namespace HomeAppDataAccessLibrary.DataAccess.HomeModelDataAccess;

public class HomeDataAccess : IHomeDataAccess
{
    private readonly ISqlDataAccess dataAccess;

    public HomeDataAccess(ISqlDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    public async Task<List<HomeModel>> GetHomeModelsById(int id)
    {
        var output = await dataAccess.LoadData<HomeModel, dynamic>(
                storedProcedure: "dbo.spSelectHomeModel",
                new { Id = id },
                connectionStringName: "Default");

        if (output.Count == 0)
        {
            return null;
        }



        foreach (var homemodel in output)
        {
            var address = await dataAccess.LoadData<AddressModel, dynamic>(
                storedProcedure: "dbo.spSelectAddressByHomeModelId",
                new { HomeModelId = homemodel.Id },
                connectionStringName: "Default");

            if (address != null)
            {
                homemodel.Address = address.FirstOrDefault();
            }

            var kitchen = await dataAccess.LoadData<KitchenModel, dynamic>(
                storedProcedure: "dbo.spSelectKitchenModel",
                new { HomeModelId = homemodel.Id },
                connectionStringName: "Default");

            if (kitchen != null)
            {
                homemodel.Kitchen.AddRange(kitchen);
            }

            var bathroom = await dataAccess.LoadData<BathRoomModel, dynamic>(
                storedProcedure: "dbo.spSelectBathroomModel",
                new { HomeModelId = homemodel.Id },
                connectionStringName: "Default");

            if (bathroom != null)
            {
                homemodel.BathRooms.AddRange(bathroom);
            }

            var rooms = await dataAccess.LoadData<RoomModel, dynamic>(
                storedProcedure: "dbo.spSelectRoomModel",
                new { HomeModelId = homemodel.Id },
                connectionStringName: "Default");

            if (rooms != null)
            {
                homemodel.Rooms.AddRange(rooms);
            }

            var toilets = await dataAccess.LoadData<ToiletModel, dynamic>(
                storedProcedure: "dbo.spSelectToiletModel",
                new { HomeModelId = homemodel.Id },
                connectionStringName: "Default");

            if (toilets != null)
            {
                homemodel.Toilet.AddRange(toilets);
            }
        }

        return output;
    }

    public async Task<HomeModel> CreateHomeModel(CreateHomeModelDTO createModel)
    {
        var output = await dataAccess.LoadData<HomeModel, dynamic>(
        storedProcedure: "dbo.spInsertHomeModel",
        new { Name = createModel.Name, Description = createModel.Description, UserId = createModel.UserId },
        connectionStringName: "Default");

        if(output == null)
        {
            return null;
        }        

        var model =  output.FirstOrDefault();

        if (createModel.Address is not null)
        {
                await dataAccess.SaveData<dynamic>(
                storedProcedure: "dbo.spInsertAddress",
                new { Id = model.Id, Country = createModel.Address.Country, City = createModel.Address.City, Street = createModel.Address.Street },
                connectionStringName: "Default");
        }

        if (createModel.Kitchen.Count > 0) 
        {
            foreach(var kitchen in createModel.Kitchen)
            {
                await dataAccess.SaveData<dynamic>(
                storedProcedure: "dbo.spInsertKitchenModel",
                new
                {
                    Sink = kitchen.Sink,
                    Oven = kitchen.Oven,
                    Fridge = kitchen.Fridge,
                    OtherElectronics = kitchen.OtherElectronics,
                    Lights = kitchen.Lights,
                    Windows = kitchen.Windows,
                    Description = kitchen.Description,
                    RoomTypeId = kitchen.RoomTypeId,
                    HomeModelId = model.Id
                },
                connectionStringName: "Default");
            }
        }

        if(createModel.BathRooms.Count > 0)
        {
            foreach(var bath in createModel.BathRooms)
            {
                await dataAccess.SaveData<dynamic>(
                storedProcedure: "dbo.spInsertBathroomModel",
                new
                {
                    Tap = bath.Tap,
                    BathTap = bath.BathTap,
                    Toilet = bath.Toilet,
                    Shower = bath.Shower,
                    Lights = bath.Lights,
                    Windows = bath.Windows,
                    Description = bath.Description,
                    RoomTypeId = bath.RoomTypeId,
                    HomeModelId = model.Id
                },
                connectionStringName: "Default");
            }
        }

        if(createModel.Toilet.Count > 0)
        {
            foreach(var toilet in createModel.Toilet)
            {
                await dataAccess.SaveData<dynamic>(
                storedProcedure: "dbo.spInsertToiletModel",
                new
                {
                    Tap = toilet.Tap,
                    Toilet = toilet.Toilet,
                    Lights = toilet.Lights,
                    Windows = toilet.Windows,
                    Description = toilet.Description,
                    RoomTypeId = toilet.RoomTypeId,
                    HomeModelId = model.Id
                },
                connectionStringName: "Default");
            }
        }

        if(createModel.Rooms.Count > 0)
        {
            foreach( var rooms in createModel.Rooms)
            {
                await dataAccess.SaveData<dynamic>(
                storedProcedure: "dbo.spInsertRoomModel",
                new
                {
                    Electronics = rooms.Electronics,
                    Lights = rooms.Lights,
                    Windows = rooms.Windows,
                    Description = rooms.Description,
                    RoomTypeId = rooms.RoomTypeId,
                    HomeModelId = model.Id
                },
                connectionStringName: "Default");
            }
        }

        var fullhomemodel = await GetHomeModelsById(model.Id);

        if(fullhomemodel != null)
        {
                var newHomeModel = fullhomemodel.FirstOrDefault();

                await dataAccess.SaveData<dynamic>(
                storedProcedure: "dbo.spInsertUserAddress",
                new { UserId = newHomeModel.UserId, AddressId = newHomeModel.Address.Id },
                connectionStringName: "Default"); 
        }

        if(fullhomemodel != null)
        {
            return fullhomemodel.FirstOrDefault();
        }

        return null;
    }

    public async Task<HomeModel> UpdateHomeModel(HomeModel model)
    {
        var output = await dataAccess.LoadData<HomeModel, dynamic>(
        storedProcedure: "dbo.spUpdateHomeModel",
        new { Id = model.Id, model.Name, Description = model.Description, UserId = model.UserId },
        connectionStringName: "Default");

        if (output == null)
        {
            return null;
        }

        var homeModel = output.FirstOrDefault();

        var kitchens = await dataAccess.LoadData<KitchenModel, dynamic>(
                storedProcedure: "dbo.spSelectKitchenModel",
                new { HomeModelId = homeModel.Id },
                connectionStringName: "Default");


        if (kitchens != null)
        {
            homeModel.Kitchen.AddRange(kitchens);
        }

        var bathrooms = await dataAccess.LoadData<BathRoomModel, dynamic>(
                storedProcedure: "dbo.spSelectBathroomModel",
                new { HomeModelId = homeModel.Id },
                connectionStringName: "Default");


        if (bathrooms != null)
        {
            homeModel.BathRooms.AddRange(bathrooms);
        }

        var toilets = await dataAccess.LoadData<ToiletModel, dynamic>(
                storedProcedure: "dbo.spSelectToiletModel",
                new { HomeModelId = homeModel.Id },
                connectionStringName: "Default");


        if (toilets != null)
        {
            homeModel.Toilet.AddRange(toilets);
        }

        var rooms = await dataAccess.LoadData<RoomModel, dynamic>(
                storedProcedure: "dbo.spSelectRoomModel",
                new { HomeModelId = homeModel.Id },
                connectionStringName: "Default");


        if (rooms != null)
        {
            homeModel.Rooms.AddRange(rooms);
        }

        await UpdateKitchenModels(homeModel, model);

        await UpdateBathroomModels(homeModel, model);

        await UpdateToiletModels(homeModel, model);

        await UpdateRoomModels(homeModel, model);

         var fullhomemodel = await GetHomeModelsById(homeModel.Id);

        if (fullhomemodel != null)
        {
            return fullhomemodel.FirstOrDefault();
        }

        return null;
    }

    public async Task DeleteHomeModel(int id)
    {
         await dataAccess.SaveData<dynamic>(
            storedProcedure: "dbo.spDeleteHomeModelById",
            new { Id = id },
            connectionStringName: "Default");
    }

    private async Task UpdateKitchenModels(HomeModel homeModel, HomeModel model)
    {
        var newHomeModelIds = model.Kitchen.Select(x => x.Id);

        var oldHomeModelIds = homeModel.Kitchen.Select(x => x.Id);

        if (model.Kitchen.Count == 0)
        {
            foreach (var exhome in homeModel.Kitchen)
            {
                await dataAccess.SaveData<dynamic>(
                storedProcedure: "dbo.spDeleteKitchenModel",
                new { Id = exhome.Id },
                connectionStringName: "Default");
            }
        }

        if(homeModel.Kitchen.Count > 0)
        {
            foreach (var newModel in model.Kitchen)
            {
                if (!oldHomeModelIds.Contains(newModel.Id))
                {
                    await dataAccess.SaveData<dynamic>(
                        storedProcedure: "dbo.spInsertKitchenModel",
                        new
                        {
                            Sink = newModel.Sink,
                            Oven = newModel.Oven,
                            Fridge = newModel.Fridge,
                            OtherElectronics = newModel.OtherElectronics,
                            Lights = newModel.Lights,
                            Windows = newModel.Windows,
                            Description = newModel.Description,
                            RoomTypeId = newModel.RoomTypeId,
                            HomeModelId = homeModel.Id
                        },
                        connectionStringName: "Default");
                }

                if (oldHomeModelIds.Contains(newModel.Id))
                {
                    await dataAccess.SaveData<dynamic>(
                            storedProcedure: "dbo.spUpdateKitchenModel",
                            new
                            {
                                Sink = newModel.Sink,
                                Oven = newModel.Oven,
                                Fridge = newModel.Fridge,
                                OtherElectronics = newModel.OtherElectronics,
                                Lights = newModel.Lights,
                                Windows = newModel.Windows,
                                Description = newModel.Description,
                                RoomTypeId = newModel.RoomTypeId,
                                HomeModelId = homeModel.Id,
                                Id = newModel.Id
                            },
                            connectionStringName: "Default");
                }
            }

            foreach (var oldkitchenId in oldHomeModelIds)
            {
                if (!newHomeModelIds.Contains(oldkitchenId))
                {
                    await dataAccess.SaveData<dynamic>(
                    storedProcedure: "dbo.spDeleteKitchenModel",
                    new { Id = oldkitchenId },
                    connectionStringName: "Default");
                }
            }
        }
    }

    private async Task UpdateBathroomModels(HomeModel homeModel, HomeModel model)
    {
        var newHomeModelIds = model.BathRooms.Select(x => x.Id);

        var oldHomeModelIds = homeModel.BathRooms.Select(x => x.Id);

        if (model.BathRooms.Count == 0)
        {
            foreach (var exhome in homeModel.BathRooms)
            {
                await dataAccess.SaveData<dynamic>(
                storedProcedure: "dbo.spDeleteBathRoomModel",
                new { Id = exhome.Id },
                connectionStringName: "Default");
            }
        }

        if (homeModel.BathRooms.Count > 0)
        {
            foreach (var newModel in model.BathRooms)
            {
                if (!oldHomeModelIds.Contains(newModel.Id))
                {
                    await dataAccess.SaveData<dynamic>(
                    storedProcedure: "dbo.spInsertBathroomModel",
                    new
                    {
                        Tap = newModel.Tap,
                        BathTap = newModel.BathTap,
                        Toilet = newModel.Toilet,
                        Shower = newModel.Shower,
                        Lights = newModel.Lights,
                        Windows = newModel.Windows,
                        Description = newModel.Description,
                        RoomTypeId = newModel.RoomTypeId,
                        HomeModelId = homeModel.Id
                    },
                    connectionStringName: "Default");
                }

                if (oldHomeModelIds.Contains(newModel.Id))
                {
                    await dataAccess.SaveData<dynamic>(
                        storedProcedure: "dbo.spUpdateBathRoomModel",
                        new
                        {
                            Tap = newModel.Tap,
                            BathTap = newModel.BathTap,
                            Toilet = newModel.Toilet,
                            Shower = newModel.Shower,
                            Lights = newModel.Lights,
                            Windows = newModel.Windows,
                            Description = newModel.Description,
                            RoomTypeId = newModel.RoomTypeId,
                            HomeModelId = homeModel.Id,
                            Id = newModel.Id
                        },
                        connectionStringName: "Default");
                }
            }

            foreach (var oldbathId in oldHomeModelIds)
            {
                if (!newHomeModelIds.Contains(oldbathId))
                {
                    await dataAccess.SaveData<dynamic>(
                    storedProcedure: "dbo.spDeleteBathRoomModel",
                    new { Id = oldbathId },
                    connectionStringName: "Default");
                }
            }
        }
    }

    private async Task UpdateToiletModels(HomeModel homeModel, HomeModel model)
    {
        var newHomeModelIds = model.Toilet.Select(x => x.Id);

        var oldHomeModelIds = homeModel.Toilet.Select(x => x.Id);

        if (homeModel.Toilet.Count > 0)
        {
            foreach (var newModel in model.Toilet)
            {
                if (!oldHomeModelIds.Contains(newModel.Id))
                {
                    await dataAccess.SaveData<dynamic>(
                    storedProcedure: "dbo.spInsertToiletModel",
                    new
                    {
                        Tap = newModel.Tap,
                        Toilet = newModel.Toilet,
                        Lights = newModel.Lights,
                        Windows = newModel.Windows,
                        Description = newModel.Description,
                        RoomTypeId = newModel.RoomTypeId,
                        HomeModelId = homeModel.Id
                    },
                    connectionStringName: "Default");
                }

                if (oldHomeModelIds.Contains(newModel.Id))
                {
                    await dataAccess.SaveData<dynamic>(
                        storedProcedure: "dbo.spUpdateToiletModel",
                        new
                        {
                            Tap = newModel.Tap,
                            Toilet = newModel.Toilet,
                            Lights = newModel.Lights,
                            Windows = newModel.Windows,
                            Description = newModel.Description,
                            RoomTypeId = newModel.RoomTypeId,
                            HomeModelId = homeModel.Id,
                            Id = newModel.Id
                        },
                        connectionStringName: "Default");
                }
            }

            foreach (var oldtoiId in oldHomeModelIds)
            {
                if (!newHomeModelIds.Contains(oldtoiId))
                {
                    await dataAccess.SaveData<dynamic>(
                    storedProcedure: "dbo.spDeleteBathRoomModel",
                    new { Id = oldtoiId },
                    connectionStringName: "Default");
                }
            }
        }
    }

    private async Task UpdateRoomModels(HomeModel homeModel, HomeModel model)
    {
        var newHomeModelIds = model.Rooms.Select(x => x.Id);

        var oldHomeModelIds = homeModel.Rooms.Select(x => x.Id);

        if (model.Rooms.Count == 0)
        {
            foreach (var exhome in homeModel.Rooms)
            {
                await dataAccess.SaveData<dynamic>(
                storedProcedure: "dbo.spDeleteRoomModel",
                new { Id = exhome.Id },
                connectionStringName: "Default");
            }
        }

        if (model.Rooms.Count > 0)
        {
            foreach (var newModel in model.Rooms)
            {
                if (!oldHomeModelIds.Contains(newModel.Id))
                {
                    await dataAccess.SaveData<dynamic>(
                    storedProcedure: "dbo.spInsertRoomModel",
                    new
                    {
                        Electronics = newModel.Electronics,
                        Lights = newModel.Lights,
                        Windows = newModel.Windows,
                        Description = newModel.Description,
                        RoomTypeId = newModel.RoomTypeId,
                        HomeModelId = homeModel.Id
                    },
                    connectionStringName: "Default");
                }

                if (oldHomeModelIds.Contains(newModel.Id))
                {
                    await dataAccess.SaveData<dynamic>(
                        storedProcedure: "dbo.spUpdateRoomModel",
                        new
                        {
                            Electronics = newModel.Electronics,
                            Lights = newModel.Lights,
                            Windows = newModel.Windows,
                            Description = newModel.Description,
                            RoomTypeId = newModel.RoomTypeId,
                            HomeModelId = homeModel.Id,
                            Id = newModel.Id
                        },
                        connectionStringName: "Default");
                }
            }

            foreach (var oldroomId in oldHomeModelIds)
            {
                if (!newHomeModelIds.Contains(oldroomId))
                {
                    await dataAccess.SaveData<dynamic>(
                    storedProcedure: "dbo.spDeleteRoomModel",
                    new { Id = oldroomId },
                    connectionStringName: "Default");
                }
            }
        }
    }
}
