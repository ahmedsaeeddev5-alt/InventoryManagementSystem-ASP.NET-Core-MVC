using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
    T GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}