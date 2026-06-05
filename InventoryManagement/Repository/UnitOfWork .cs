using InventoryManagement.Models;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IRepository<Product> Products { get; private set; }
    public IRepository<Category> Categories { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Products = new Repository<Product>(_context);
        Categories = new Repository<Category>(_context);
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}