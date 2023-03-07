using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppDataAccessLibrary.Models.DTOs.AddressDTO
{
    public class ReadAddressDTO
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int UserId { get; set; }

        public List<ReadHomeModelDTO> HomeModels { get; set; } = new List<ReadHomeModelDTO>();
    }
}
