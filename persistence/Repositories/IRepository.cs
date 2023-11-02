using System.Linq.Expressions;

namespace codemazepractice.persistence.Repositories;

public interface IRepository<T>
{
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
}