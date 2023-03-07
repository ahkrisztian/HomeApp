using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.DTOs.AddressDTO;
using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppDataAccessLibrary.Models.DTOs.UserDTO
{
    public class ReadUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<ReadAddressDTO> Addresses { get; set; } = new List<ReadAddressDTO>();

        public List<ReadHomeModelDTO> HomeModels { get; set; } = new List<ReadHomeModelDTO>();
    }
}
