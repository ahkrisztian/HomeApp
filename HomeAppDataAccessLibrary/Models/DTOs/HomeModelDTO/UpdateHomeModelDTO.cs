using HomeAppDataAccessLibrary.Models.DTOs.SubDTOs;
using HomeAppDataAccessLibrary.Models.RoomModels;

namespace HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;

public class UpdateHomeModelDTO
{
    public string Name { get; set; }
    public string Description { get; set; }

    public List<BathRoomModel> BathRooms { get; set; } = new List<BathRoomModel>();
    public List<KitchenModel> Kitchen { get; set; } = new List<KitchenModel>();

    public List<ToiletModel> Toilet { get; set; } = new List<ToiletModel>();
    public List<RoomModel> Rooms { get; set; } = new List<RoomModel>();
}
