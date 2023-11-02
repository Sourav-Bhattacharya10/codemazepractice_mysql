using codemazepractice.domain;
using Microsoft.EntityFrameworkCore;

namespace codemazepractice.persistence.Repositories;

public class AccountRepository: Repository<Account>, IAccountRepository
{
    private readonly CodeMazeContext _context;

    public async Task<Account?> FindOneAsync(Guid id)
    {
        return await _context.Account.AsNoTracking().FirstOrDefaultAsync(entity => entity.AccountID == id);
    }

    
    public async Task<bool> ExistsAsync(Guid id)
    {
        Account? entity = await FindOneAsync(id);

        return entity != null;
    }

    public AccountRepository(CodeMazeContext context): base(context)
    {
        _context = context;
    }
}