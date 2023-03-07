using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppDataAccessLibrary.Models.DTOs.SubDTOs
{
    public class CreateRoomDTO : CreateRoomBaseDTO
    {
        public bool Electronics { get; set; }

        public int RoomTypeId { get; set; }
    }
}
