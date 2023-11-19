using codemazepractice.domain;
using codemazepractice.domain.DTO;
using Microsoft.AspNetCore.JsonPatch;

namespace codemazepractice.persistence.Repositories;

public interface IOwnerRepository : IRepository<Owner>
{
    Task<Owner?> FindOneAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<IEnumerable<OwnerDto>> GetAllOwnersWithAssociations();
    Task<OwnerDto> GetOwnerWithAssociations(Guid ownerID);
    Task<OwnerDto?> PatchOwnerProperties(Guid ownerID, JsonPatchDocument<Owner> ownerModel);
}