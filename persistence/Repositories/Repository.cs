using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace codemazepractice.persistence.Repositories;

public class Repository<T>: IRepository<T> where T: class
{
    private readonly CodeMazeContext _context;

    public Repository(CodeMazeContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> FindAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
    {
        IQueryable<T> conditionalResults = _context.Set<T>().Where(expression);
        return await conditionalResults.AsNoTracking().ToListAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);

        return entity;
    }

    public T Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;

        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);

        return entity;
    }
}