using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.HomeModels;
using System.ComponentModel.DataAnnotations;

namespace HomeAppDataAccessLibrary.Models.UserModels;

public class UserModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [Required]
    [MaxLength(100)]
    public string ObjectId { get; set; }

    public List<AddressModel> Addresses { get; set; } = new List<AddressModel>();

    public List<HomeModel> HomeModels { get; set; } = new List<HomeModel>();

}
