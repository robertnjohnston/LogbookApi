using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LogbookApi.UnityOfWork.Implementation
{
    public class UnitOfWorkAsync : IUnitOfWorkAsync
    {
        private readonly DbContext _context;

        public UnitOfWorkAsync(Type contextType, string connectionString)
        {
            _context = (DbContext)Activator.CreateInstance(contextType, connectionString);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepositoryAsync<T> GetRepository<T>() where T : class
        {
            return new RepositoryAsync<T>(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}