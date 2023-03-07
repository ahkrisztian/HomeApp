using HomeAppDataAccessLibrary.Models.RoomModels;
using System.ComponentModel.DataAnnotations;

namespace HomeAppDataAccessLibrary.Models.HomeModels;

public class HomeModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int AddressId { get; set; }

    public List<BathRoomModel> BathRooms { get; set;} = new List<BathRoomModel>();
    public List<KitchenModel> Kitchen { get; set; } = new List<KitchenModel>();
    public List<ToiletModel> Toilet { get; set; } = new List<ToiletModel>();
    public List<RoomModel> Rooms { get; set;} = new List<RoomModel>();
}
