using codemazepractice.domain;
using Microsoft.EntityFrameworkCore;

namespace codemazepractice.persistence.Repositories;

public class ImageInfoRepository : Repository<ImageInfo>, IImageInfoRepository
{
    private readonly CodeMazeContext _context;
    public ImageInfoRepository(CodeMazeContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ImageInfo?> FindOneAsync(Guid id)
    {
        return await _context.ImageInfo.AsNoTracking().FirstOrDefaultAsync(entity => entity.ImageID == id);
    }

    
    public async Task<bool> ExistsAsync(Guid id)
    {
        ImageInfo? entity = await FindOneAsync(id);

        return entity != null;
    }
}