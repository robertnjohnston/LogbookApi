namespace UnityOfWork
{
    public class UnitOfWorkFactoryProvider : IUnitOfWorkFactoryProvider
    {
        public IUnitOfWorkFactory Create(string contextTypeName, string connectionString)
        {
            return new UnitOfWorkFactory(contextTypeName, connectionString);
        }
    }
}