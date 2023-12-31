using codemazepractice.persistence.Repositories;

namespace codemazepractice.persistence.Contracts;

public interface IUnitOfWork : IDisposable
{
    AccountRepository AccountRepository { get; }
    OwnerRepository OwnerRepository { get; }

    ImageInfoRepository ImageInfoRepository { get; }

    Task SaveChangesAsync();
}