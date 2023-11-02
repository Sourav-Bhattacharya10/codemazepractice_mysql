using AutoMapper;
using codemazepractice.domain;
using codemazepractice.domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace codemazepractice.persistence.Repositories;

public class OwnerRepository: Repository<Owner>
{
    private readonly IMapper _mapper;
    private readonly CodeMazeContext _context;

    public OwnerRepository(CodeMazeContext context, IMapper mapper): base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Owner?> FindOneAsync(Guid id)
    {
        return await _context.Owner.AsNoTracking().FirstOrDefaultAsync(entity => entity.OwnerID == id);
    }

    
    public async Task<bool> ExistsAsync(Guid id)
    {
        Owner? entity = await FindOneAsync(id);

        return entity != null;
    }

    public async Task<IEnumerable<OwnerDto>> GetAllOwnersWithAssociatedAccounts()
    {
        var accountSet = _context.Account;
        var ownerSet = _context.Owner;

        var owners = await ownerSet.Select(owner => new Owner { OwnerID = owner.OwnerID, Name = owner.Name, DateOfBirth = owner.DateOfBirth, Address = owner.Address, Accounts = accountSet.Where(ac => ac.OwnerID == owner.OwnerID).ToList() }).ToListAsync();
        var ownerDtos = _mapper.Map<List<OwnerDto>>(owners);

        return ownerDtos;
    } 
}