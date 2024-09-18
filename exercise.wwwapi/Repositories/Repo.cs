using System.Linq.Expressions;
using exercise.wwwapi.Data;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repositories;

public class Repo<T>(DataContext db) : IRepo<T> where T : class
{
    private DbSet<T> _table = db.Set<T>();
    public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
    {
        if (includeExpressions.Any())
        {
            var set = includeExpressions
                .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                    (_table, (current, expression) => current.Include(expression));
        }
        return _table.ToList();
    }

    public IEnumerable<T> GetAll()
    {
        return _table.ToList();
    }
    public T GetById(object id)
    {
        return _table.Find(id);
    }

    public void Insert(T obj)
    {
        _table.Add(obj);
    }
    public void Update(T obj)
    {
        _table.Attach(obj);
        db.Entry(obj).State = EntityState.Modified;
    }

    public void Delete(object id)
    {
        T existing = _table.Find(id);
        _table.Remove(existing);
    }
    
    public void Save()
    {
        db.SaveChanges();
    }
    public DbSet<T> Table { get { return _table; } }

}