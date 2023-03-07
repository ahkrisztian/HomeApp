using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppDataAccessLibrary.Models.DTOs.SubDTOs
{
    public class CreateRoomBaseDTO
    {
        public bool Lights { get; set; }
        public bool Windows { get; set; }
        public string Description { get; set; }
    }
}
