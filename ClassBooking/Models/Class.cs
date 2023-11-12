using System.ComponentModel.DataAnnotations;

namespace ClassBooking.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public string ClassTitle { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime CheckInTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime CheckOutTime { get; set; }
        public string? Credit { get; set; }
        public string? NumberOfPeople { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
