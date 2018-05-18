using System;
using System.Threading.Tasks;

namespace LogbookApi.UnityOfWork
{
    public interface IUnitOfWorkAsync : IDisposable
    {
        IRepositoryAsync<T> GetRepository<T>() where T : class;

        Task<int> SaveAsync();
    }
}