using System;

namespace LogbookApi.UnityOfWork.Implementation
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly Type _contextType;
        private readonly string _connectionString;

        public UnitOfWorkFactory(string contextTypeName, string connectionString)
        {
            _contextType = Type.GetType(contextTypeName);
            _connectionString = connectionString;
        }

        public IUnitOfWorkAsync CreateAsync()
        {
            return new UnitOfWorkAsync(_contextType, _connectionString);
        }
    }
}