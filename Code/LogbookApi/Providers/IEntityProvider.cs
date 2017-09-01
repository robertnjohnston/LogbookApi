using System.Collections.Generic;

namespace LogbookApi.Providers
{
    public interface IEntityProvider<T>
    {
        IEnumerable<T> Get();

        T Get(int id);

        T Get(string name);

        T Save(T entity);
    }
}