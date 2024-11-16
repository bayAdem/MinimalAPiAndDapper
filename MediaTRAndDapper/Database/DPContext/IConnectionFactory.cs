using System.Data;

namespace MediaTRAndDapper.Database.DPContext
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
