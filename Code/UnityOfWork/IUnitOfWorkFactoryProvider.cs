namespace UnityOfWork
{
    public interface IUnitOfWorkFactoryProvider
    {
        IUnitOfWorkFactory Create(string contextTypeName, string connectionString);
    }
}