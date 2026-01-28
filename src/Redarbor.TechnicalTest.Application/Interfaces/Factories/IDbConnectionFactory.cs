namespace Redarbor.TechnicalTest.Application.Interfaces.Factories;

public interface IDbConnectionFactory
{
    public IDbConnection CreateConnection();
}
