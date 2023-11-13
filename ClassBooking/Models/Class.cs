using System.ComponentModel.DataAnnotations;

namespace ClassBooking.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public int AdminID { get; set; }
        public string ClassTitle { get; set; }
        public string Date { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public int Credit { get; set; }
        public int NumberOfPeople { get; set; }
        public string Country { get; set; }
        public string ExpiredDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int StatusFlag { get; set; }
        public List<Class> listClass { get; set; }
    }
}
