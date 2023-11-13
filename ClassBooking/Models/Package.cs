namespace ClassBooking.Models
{
    public class Package
    {
        public int PackageID { get; set; }
        public int AdminID { get; set; }
        public string PackageName { get; set; }
        public string Country { get; set; }
        public int Price { get; set; }
        public int Credit { get; set; }
        public string ExpiredDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }

        public bool PurchasedFlag { get; set; }
        public List<Package> listPackage { get; set; }
    }
}
