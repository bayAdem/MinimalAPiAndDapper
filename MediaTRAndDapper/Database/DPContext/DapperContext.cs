using System.Data;
using System.Data.SqlClient;

namespace MediaTRAndDapper.Database.DPContext
{
    public class DapperContext : IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection"); // Or the key you use in appsettings.json
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }

}
