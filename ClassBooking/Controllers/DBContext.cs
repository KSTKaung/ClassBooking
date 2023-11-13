using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ClassBooking.Controllers
{
    public class DBContext
    {
        public IConfiguration Configuration { get; }
        public DBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
