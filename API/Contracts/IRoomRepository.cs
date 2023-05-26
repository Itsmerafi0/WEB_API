using API.Models;
using API.ViewModels.Rooms;

namespace API.Contracts
{
    public interface IRoomRepository : IGeneralRepository <Room>
    {
        IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);
        IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();

        // Kelompok 4
        IEnumerable<RoomBookedTodayVM> GetAvailableRoom();

        public string GetRoomStatus(Booking booking, DateTime dateTime);
    }
}
