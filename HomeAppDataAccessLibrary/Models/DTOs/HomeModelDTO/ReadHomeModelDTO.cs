using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.RoomModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO
{
    public class ReadHomeModelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public AddressModel Address { get; set; }
        public List<BathRoomModel> BathRooms { get; set; } = new List<BathRoomModel>();
        public List<KitchenModel> Kitchen { get; set; } = new List<KitchenModel>();
        public List<ToiletModel> Toilet { get; set; } = new List<ToiletModel>();
        public List<RoomModel> Rooms { get; set; } = new List<RoomModel>();
    }
}
