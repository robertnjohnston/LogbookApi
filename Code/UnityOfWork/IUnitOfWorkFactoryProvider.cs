namespace LogbookApi.UnityOfWork
{
    public interface IUnitOfWorkFactoryProvider
    {
        IUnitOfWorkFactory Create(string contextTypeName, string connectionString);
    }
}