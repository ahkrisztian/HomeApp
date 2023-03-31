namespace HomeAppDataAccessLibrary.Models.DTOs.AddressDTO;

public class ReadAddressDTO
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public override string ToString()
    {
        return $"{Country} {City} {Street}";
    }
}
