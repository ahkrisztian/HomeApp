using HomeAppDataAccessLibrary.Models.DTOs.AddressDTO;
using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;

namespace HomeAppDataAccessLibrary.Models.DTOs.UserDTO;

public class ReadUserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public List<ReadAddressDTO> Addresses { get; set; } = new List<ReadAddressDTO>();

    public List<ReadHomeModelDTO> HomeModels { get; set; } = new List<ReadHomeModelDTO>();
}
