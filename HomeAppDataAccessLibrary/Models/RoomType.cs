using HomeAppDataAccessLibrary.Models.RoomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppDataAccessLibrary.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public string RoomTypeName { get; set; }
        public ICollection<RoomModel> RoomModels { get; set; }
    }
}
