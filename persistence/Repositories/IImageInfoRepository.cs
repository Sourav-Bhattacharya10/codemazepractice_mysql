using codemazepractice.domain;

namespace codemazepractice.persistence.Repositories;

public interface IImageInfoRepository : IRepository<ImageInfo>
{
    Task<ImageInfo?> FindOneAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}