using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppDataAccessLibrary.Models.DTOs.SubDTOs
{
    public class CreateBathroomDTO : CreateRoomBaseDTO
    {
        public bool Tap { get; set; }
        public bool BathTap { get; set; }
        public bool Toilet { get; set; }
        public bool Shower { get; set; }
        public int RoomTypeId { get; set; } = 10;
    }
}
