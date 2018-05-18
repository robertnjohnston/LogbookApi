namespace LogbookApi.UnityOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWorkAsync CreateAsync();
    }
}