using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ClassBooking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClassBooking.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    public class User : Controller
    {
        public IConfiguration Configuration { get; }
        public User(IConfiguration configuration)

        {
            Configuration = configuration;
        }

        /// <summary>
        /// Login Controller
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Register Controller
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Login Process
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        public void LoginData(UserModel user)
        {
            TempData["ErrorMessage"] = "";
            try
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM tbl_USER WHERE USERNAME = @username AND PASSWORD = @password";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", user.UserName);
                        command.Parameters.AddWithValue("@password", user.Password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                HttpContext.Session.SetInt32(ConstantData.UserID,reader.GetInt32(0));
                                HttpContext.Session.SetInt32(ConstantData.UserCountry, reader.GetInt32(4));
                                HttpContext.Session.SetInt32(ConstantData.UserRemainCredit, reader.GetInt32(7));
                                Response.Redirect("/Home/Package");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return;
            }
        }

        /// <summary>
        /// Register Process
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        public void RegisterData(UserModel user)
        {
            try
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "INSERT INTO tbl_USER" +
                                 "(UserName,Password,Name,Country,PhoneNo,Address,RemainCredit,CreatedDateTime,UpdatedDateTime) VALUES" +
                                 "(@username,@password,@name,@country,@phoneno,@address,@remaincredit,@createddatetime,@updateddatetime);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", user.UserName);
                        command.Parameters.AddWithValue("@password", user.Password);
                        command.Parameters.AddWithValue("@name", user.Name);
                        command.Parameters.AddWithValue("@country", user.Country);
                        command.Parameters.AddWithValue("@phoneno", user.PhoneNo);
                        command.Parameters.AddWithValue("@address", user.Address);
                        command.Parameters.AddWithValue("@remaincredit", user.RemainCredit);
                        command.Parameters.AddWithValue("@createddatetime", DateTime.Now);
                        command.Parameters.AddWithValue("@updateddatetime", DateTime.Now);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                return;
            }

            Response.Redirect("/User/Login");
        }
    }
}
