using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Rooms
{
    public class RoomVM
    {
        public Guid? Guid { get; set; }
        public string Name { get; set; }

        [Required ]
        public int Floor { get; set; }
        public int Capacity { get; set; }
    }
}
