using System.Collections.Generic;

namespace LogbookApi.Providers
{
    public interface IEntityProvider<T>
    {
        IEnumerable<T> Get();

        T Get(int id);

        T Save(T entity);
    }
}