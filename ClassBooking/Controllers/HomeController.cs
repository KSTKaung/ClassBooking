using ClassBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using ClassBooking.Models;
using System.Runtime.InteropServices;
using System.Globalization;

namespace ClassBooking.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }

        public List<Class> lstClass = new List<Class>();

        public List<int> lstBookedClassID = new List<int>();
        // Unbooked = 1
        // WaitList = 2
        // Cancel   = 3
        // Booked   = 4
        public List<int> lstBookedStatus = new List<int>();

        public List<DateTime> lstClassCheckInDateTime = new List<DateTime>();
        public List<DateTime> lstClassCheckOutDateTime = new List<DateTime>();

        public List<int> lstPurchasedPackageID = new List<int>();

        //Class Info
        public DateTime dtBookCheckIn, dtBookCheckOut;
        public DateTime dtBookedCheckIn, dtBookedCheckOut;
        public bool overlapFlag = false;

        public List<Package> lstPackage = new List<Package>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;

            Configuration = configuration;
        }

        /// <summary>
        /// Class Controller
        /// </summary>
        /// <returns></returns>
        public ActionResult Class()
        {
            Class LatestClassinfo = new Class();
            try
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = String.Empty;
                    if (HttpContext.Session.GetInt32(ConstantData.UserID) == null)
                        sql = "SELECT * FROM tbl_Class";
                    else
                    {
                        sql = "SELECT CLASSID,STATUS FROM tbl_Booking WHERE USERID = @userid ";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@userid", HttpContext.Session.GetInt32(ConstantData.UserID));

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    lstBookedClassID.Add(reader.GetInt32(0));
                                    lstBookedStatus.Add(reader.GetInt32(1));
                                }
                            }
                        }
                        sql = "SELECT * FROM tbl_Class WHERE COUNTRY = @usercountry";
                    }

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if (HttpContext.Session.GetInt32(ConstantData.UserID) != null)
                            command.Parameters.AddWithValue("@usercountry", HttpContext.Session.GetInt32(ConstantData.UserCountry));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                Class classinfo = new Class();
                                classinfo.ClassID = reader.GetInt32(0);
                                classinfo.AdminID = reader.GetInt32(1);
                                classinfo.ClassTitle = reader.GetString(2);
                                classinfo.Date = reader.GetDateTime(3).ToString();
                                classinfo.CheckInTime = reader.GetString(4);
                                classinfo.CheckOutTime = reader.GetString(5);
                                classinfo.Credit = reader.GetInt32(6);
                                classinfo.NumberOfPeople = reader.GetInt32(7);
                                classinfo.Country = Enum.GetName(typeof(ConstantData.Country), reader.GetInt32(8));
                                classinfo.ExpiredDate = reader.GetDateTime(9).ToString();
                                classinfo.CreatedDateTime = reader.GetDateTime(10);
                                classinfo.UpdatedDateTime = reader.GetDateTime(11);

                                if (lstBookedClassID.Contains(classinfo.ClassID))
                                    classinfo.StatusFlag = lstBookedStatus[lstBookedClassID.IndexOf(classinfo.ClassID)];

                                lstClass.Add(classinfo);
                            }
                            LatestClassinfo.listClass = lstClass;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View(LatestClassinfo);
        }

        /// <summary>
        /// Booking Class Process
        /// </summary>
        /// <param name="id"></param>
        public void BookClass(string id)
        {
            if (HttpContext.Session.GetInt32(ConstantData.UserID) == null)
            {
                TempData["ErrorMessage"] = "You Need To Login First";
                Response.Redirect("/Home/Class");
            }
            else
            {
                try
                {
                    var connectionString = Configuration.GetConnectionString("DefaultConnection");

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        String sql = String.Empty;

                        sql = "SELECT tbl_Class.Date, tbl_Class.CheckInTime, tbl_Class.CheckOutTime " +
                              "FROM tbl_Booking RIGHT JOIN tbl_Class ON tbl_Booking.CLASSID = tbl_Class.CLASSID " +
                              "WHERE tbl_Booking.USERID = @userid AND tbl_Booking.STATUS = @status1 OR tbl_Booking.STATUS = @status2";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@userid", HttpContext.Session.GetInt32(ConstantData.UserID));
                            command.Parameters.AddWithValue("@status1", 2);
                            command.Parameters.AddWithValue("@status2", 4);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string strClassDate = reader.GetDateTime(0).ToString().Split(' ')[0];
                                    string strClassCheckInDateTime = reader.GetString(1);
                                    string strClassCheckOutDateTime = reader.GetString(2);

                                    dtBookedCheckIn = new DateTime(Int32.Parse(strClassDate.Split('/')[2]), Int32.Parse(strClassDate.Split('/')[0]), Int32.Parse(strClassDate.Split('/')[1]), Int32.Parse(strClassCheckInDateTime.Split(':')[0]), Int32.Parse(strClassCheckInDateTime.Split(':')[1]), 0);
                                    lstClassCheckInDateTime.Add(dtBookedCheckIn);

                                    dtBookedCheckOut = new DateTime(Int32.Parse(strClassDate.Split('/')[2]), Int32.Parse(strClassDate.Split('/')[0]), Int32.Parse(strClassDate.Split('/')[1]), Int32.Parse(strClassCheckOutDateTime.Split(':')[0]), Int32.Parse(strClassCheckOutDateTime.Split(':')[1]), 0);
                                    lstClassCheckOutDateTime.Add(dtBookedCheckOut);
                                }
                            }
                        }
                        sql = "SELECT DATE, CHECKINTIME, CHECKOUTTIME FROM tbl_Class WHERE CLASSID = @classid";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@classid", id);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string strClassDate = reader.GetDateTime(0).ToString().Split(' ')[0];
                                    string strClassCheckInDateTime = reader.GetString(1);
                                    string strClassCheckOutDateTime = reader.GetString(2);

                                    dtBookCheckIn = new DateTime(Int32.Parse(strClassDate.Split('/')[2]), Int32.Parse(strClassDate.Split('/')[0]), Int32.Parse(strClassDate.Split('/')[1]), Int32.Parse(strClassCheckInDateTime.Split(':')[0]), Int32.Parse(strClassCheckInDateTime.Split(':')[1]), 0);

                                    dtBookCheckOut = new DateTime(Int32.Parse(strClassDate.Split('/')[2]), Int32.Parse(strClassDate.Split('/')[0]), Int32.Parse(strClassDate.Split('/')[1]), Int32.Parse(strClassCheckOutDateTime.Split(':')[0]), Int32.Parse(strClassCheckOutDateTime.Split(':')[1]), 0);
                                
                                }
                            }
                        }
                        for(int i=0; i < lstClassCheckInDateTime.Count; i++)
                        {
                            int result = DateTime.Compare(lstClassCheckInDateTime[i], dtBookCheckIn);
                            if(result < 0 || result == 0)
                            {
                                for (int j = 0; j < lstClassCheckOutDateTime.Count; j++)
                                {
                                    result = DateTime.Compare(dtBookCheckOut, lstClassCheckOutDateTime[j]);
                                    if (result < 0 || result == 0)
                                    {
                                        overlapFlag = true;
                                        TempData["ErrorMessage"] = "Cannot Book OverLap Date And Time";
                                        Response.Redirect("/Home/Class");
                                    }
                                }
                            }
                        }
                        if(!overlapFlag)
                        {
                            sql = "SELECT BOOKINGID,STATUS FROM tbl_Booking WHERE CLASSID = @classid AND USERID = @userid";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@userid", HttpContext.Session.GetInt32(ConstantData.UserID));
                                command.Parameters.AddWithValue("@classid", Int32.Parse(id));

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        int bookingID = reader.GetInt32(0);
                                        int status = reader.GetInt32(1);
                                        if (status == 3)
                                        {
                                            reader.Close();
                                            sql = "Update tbl_Booking" +
                                                  "SET BOOKINGDATE = @bookingdate, STATUS = @status, UPDATEDDATETIME = @updateddatetime" +
                                                  "WHERE BOOKINGID = @bookingid";

                                            using (SqlCommand cmd = new SqlCommand(sql, connection))
                                            {
                                                cmd.Parameters.AddWithValue("@bookingdate", DateTime.Now);
                                                cmd.Parameters.AddWithValue("@status", 4);
                                                cmd.Parameters.AddWithValue("@updateddatetime", DateTime.Now);
                                                cmd.Parameters.AddWithValue("@bookingid", bookingID);

                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        reader.Close();
                                        sql = "INSERT INTO tbl_Booking" +
                                              "(USERID,CLASSID,BOOKINGDATE,STATUS,CREATEDDATETIME,UPDATEDDATETIME) VALUES" +
                                              "(@userid,@classid,@bookingdate,@status,@createddatetime,@updateddatetime);";

                                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                                        {
                                            cmd.Parameters.AddWithValue("@userid", HttpContext.Session.GetInt32(ConstantData.UserID));
                                            cmd.Parameters.AddWithValue("@classid",Int32.Parse(id));
                                            cmd.Parameters.AddWithValue("@bookingdate", DateTime.Now);
                                            cmd.Parameters.AddWithValue("@status", 4);
                                            cmd.Parameters.AddWithValue("@createddatetime", DateTime.Now);
                                            cmd.Parameters.AddWithValue("@updateddatetime", DateTime.Now);

                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return;
                }
                Response.Redirect("/Home/Class");
            }
        }

        /// <summary>
        /// Package Controller
        /// </summary>
        /// <returns></returns>
        public ActionResult Package()
        {
            Package LatestPackageinfo = new Package();
            try
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = String.Empty;
                    if (HttpContext.Session.GetInt32(ConstantData.UserID) == null)
                        sql = "SELECT * FROM tbl_Package";
                    else
                    {
                        sql = "SELECT PackageID FROM tbl_UserPackage WHERE USERID = @userid ";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@userid", HttpContext.Session.GetInt32(ConstantData.UserID));

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    lstPurchasedPackageID.Add(reader.GetInt32(0));
                                }
                            }
                        }
                        sql = "SELECT * FROM tbl_Package WHERE COUNTRY = @usercountry";
                    }

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if (HttpContext.Session.GetInt32(ConstantData.UserID) != null)
                            command.Parameters.AddWithValue("@usercountry", HttpContext.Session.GetInt32(ConstantData.UserCountry));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                Package packageinfo = new Package();
                                packageinfo.PackageID = reader.GetInt32(0);
                                packageinfo.AdminID = reader.GetInt32(1);
                                packageinfo.PackageName = reader.GetString(2);
                                packageinfo.Country = Enum.GetName(typeof(ConstantData.Country), reader.GetInt32(3));
                                packageinfo.Price = reader.GetInt32(4);
                                packageinfo.Credit = reader.GetInt32(5);
                                packageinfo.ExpiredDate = reader.GetDateTime(6).ToString();
                                packageinfo.CreatedDateTime = reader.GetDateTime(7);
                                packageinfo.UpdatedDateTime = reader.GetDateTime(8);
                                if(lstPurchasedPackageID.Contains(packageinfo.PackageID))
                                    packageinfo.PurchasedFlag = true;

                                lstPackage.Add(packageinfo);
                            }
                            LatestPackageinfo.listPackage = lstPackage;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View(LatestPackageinfo);
        }

        /// <summary>
        /// Purchase Package Process
        /// </summary>
        /// <param name="id"></param>
        public void PurchasePackage(string id)
        {
            if (HttpContext.Session.GetInt32(ConstantData.UserID) == null)
            {
                TempData["ErrorMessage"] = "You Need To Login First";
                Response.Redirect("/Home/Package");
            }
            else 
            {
                try
                {
                    var connectionString = Configuration.GetConnectionString("DefaultConnection");

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        String sql = "INSERT INTO tbl_USERPACKAGE" +
                                     "(UserID,PackageID) VALUES" +
                                     "(@userid,@packageid);";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@userid", HttpContext.Session.GetInt32(ConstantData.UserID));
                            command.Parameters.AddWithValue("@packageid", Int32.Parse(id));

                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch(Exception e)
                {
                    return;
                }
                Response.Redirect("/Home/Package");
            }
        }

        public void LogOut()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/User/Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}