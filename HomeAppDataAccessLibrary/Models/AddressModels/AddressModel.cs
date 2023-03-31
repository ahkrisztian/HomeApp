using HomeAppDataAccessLibrary.Models.HomeModels;
using System.ComponentModel.DataAnnotations;

namespace HomeAppDataAccessLibrary.Models.AddressModels;

public class AddressModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Country { get; set; }
    [Required]
    [MaxLength(50)]
    public string City { get; set; }
    [Required]
    [MaxLength(100)]
    public string Street { get; set; }

    public override string ToString()
    {
        return $"{Country} {City} {Street}";
    }
}
