namespace ClassBooking.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public int ClassID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
