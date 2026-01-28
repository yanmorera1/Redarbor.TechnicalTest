namespace Redarbor.TechnicalTest.Infrastructure.Persistence.Factories;

public class DbConnectionFactory
    (string connectionString)
    : IDbConnectionFactory
{
    public IDbConnection CreateConnection() => new SqlConnection(connectionString);
}
