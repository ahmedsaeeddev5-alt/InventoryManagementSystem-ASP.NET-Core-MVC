using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
            query = query.Where(filter);

        return query.ToList();
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Add(T entity) => _context.Add(entity);

    public void Update(T entity) => _context.Update(entity);

    public void Delete(T entity) => _context.Remove(entity);
}