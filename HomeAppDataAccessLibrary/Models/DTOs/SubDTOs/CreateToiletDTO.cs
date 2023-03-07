
namespace HomeAppDataAccessLibrary.Models.DTOs.SubDTOs
{
    public class CreateToiletDTO : CreateRoomBaseDTO
    {
        public bool Tap { get; set; }
        public bool Toilet { get; set; }
        public int RoomTypeId { get; set; } = 9;
    }
}
