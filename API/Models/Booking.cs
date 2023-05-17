namespace API.Models
{
    public class Booking
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public DateTime  CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

    }
}
