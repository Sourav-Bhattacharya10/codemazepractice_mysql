using AutoMapper;
using codemazepractice.persistence.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace codemazepractice.persistence.Contracts;

public class UnitOfWork : IUnitOfWork
{
    private readonly CodeMazeContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private AccountRepository _accountRepository = default!;

    private OwnerRepository _ownerRepository = default!;

    private ImageInfoRepository _imageInfoRepository = default!;

    public AccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_context);

    public OwnerRepository OwnerRepository => _ownerRepository ??= new OwnerRepository(_context, _mapper, _memoryCache);

    public ImageInfoRepository ImageInfoRepository => _imageInfoRepository ??= new ImageInfoRepository(_context);

    public UnitOfWork(CodeMazeContext context, IMapper mapper, IMemoryCache memoryCache)
    {
        _context = context;
        _mapper = mapper;
        _memoryCache = memoryCache;
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