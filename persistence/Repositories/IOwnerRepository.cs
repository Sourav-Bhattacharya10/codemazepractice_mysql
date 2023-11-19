using codemazepractice.domain;
using codemazepractice.domain.DTO;

namespace codemazepractice.persistence.Repositories;

public interface IOwnerRepository : IRepository<Owner>
{
    Task<Owner?> FindOneAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<IEnumerable<OwnerDto>> GetAllOwnersWithAssociations();
    Task<OwnerDto> GetOwnerWithAssociations(Guid ownerID);
}