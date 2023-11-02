using AutoMapper;
using codemazepractice.persistence.Repositories;

namespace codemazepractice.persistence.Contracts;

public class UnitOfWork : IUnitOfWork
{
    private readonly CodeMazeContext _context;
    private readonly IMapper _mapper;

    private AccountRepository _accountRepository = default!;

    private OwnerRepository _ownerRepository = default!;

    public AccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_context);

    public OwnerRepository OwnerRepository => _ownerRepository ??= new OwnerRepository(_context, _mapper);

    public UnitOfWork(CodeMazeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}