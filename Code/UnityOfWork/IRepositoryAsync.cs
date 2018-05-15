using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnityOfWork
{
    //protected IUnitOfWorkAsync GetUnitOfWorkAsync(Guid organisationId)
    //{
    //    var factory = _unitOfWorkFactoryProvider.Create(typeof(Context).AssemblyQualifiedName, _connectionStringFactory.GetConnectionString(organisationId));
    //    return factory.CreateAsync();
    //}

    //public async Task SaveAttachmentSummary(Guid organisationId, Domain.AttachmentSummary attachmentSummary)
    //{
    //    using (var unitOfWork = GetUnitOfWorkAsync(organisationId))
    //    {
    //        var attachment = _attachmentMapper.Map(attachmentSummary);
    //        var repository = unitOfWork.GetRepository<Persisted.AttachmentSummary>();

    //        repository.Insert(attachment);

    //        await unitOfWork.SaveAsync();
    //    }
    //}

    public interface IRepositoryAsync<T> where T : class
    {

        IQueryable<T> All { get; }

        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
