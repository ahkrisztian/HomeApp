using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppDataAccessLibrary.Models.DTOs.SubDTOs
{
    public class CreateKitchenDTO : CreateRoomBaseDTO
    {
        public bool Sink { get; set; }
        public bool Fridge { get; set; }
        public bool Oven { get; set; }
        public bool OtherElectronics { get; set; }

        public int RoomTypeId { get; set; } = 8;
    }
}
