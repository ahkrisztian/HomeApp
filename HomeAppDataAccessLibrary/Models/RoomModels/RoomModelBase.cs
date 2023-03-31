namespace HomeAppDataAccessLibrary.Models.RoomModels;

public class RoomModelBase
{
    public int Id { get; set; }
    public bool Lights { get; set; }
    public bool Windows { get; set; }
    public string Description { get; set; }  
    public int HomeModelId { get; set; }
    public int RoomTypeId { get; set; }
    public string RoomType { get; set; }
}
