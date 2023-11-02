using codemazepractice.domain;

namespace codemazepractice.persistence.Repositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> FindOneAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}