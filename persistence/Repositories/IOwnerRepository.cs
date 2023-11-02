using codemazepractice.domain;

namespace codemazepractice.persistence.Repositories;

public interface IOwnerRepository : IRepository<Owner>
{
    Task<Owner?> FindOneAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}