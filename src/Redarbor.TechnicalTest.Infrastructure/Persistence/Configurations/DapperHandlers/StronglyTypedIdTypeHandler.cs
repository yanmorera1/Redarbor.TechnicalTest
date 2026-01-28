namespace Redarbor.TechnicalTest.Infrastructure.Persistence.Configurations.DapperHandlers;

public class StronglyTypedIdTypeHandler<TValueObject, TValue> : SqlMapper.TypeHandler<TValueObject>
    where TValueObject : class
{
    private readonly Func<TValue, TValueObject> _factory;

    public StronglyTypedIdTypeHandler(Func<TValue, TValueObject> factory)
    {
        _factory = factory;
    }

    public override void SetValue(IDbDataParameter parameter, TValueObject value)
    {
        parameter.Value = (value as dynamic).Value;
    }

    public override TValueObject Parse(object value)
    {
        return _factory((TValue)value);
    }
}
