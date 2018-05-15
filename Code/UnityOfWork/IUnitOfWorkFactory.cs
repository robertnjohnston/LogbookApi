namespace UnityOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWorkAsync CreateAsync();
    }
}