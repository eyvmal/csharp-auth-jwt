using System.Linq.Expressions;
using exercise.wwwapi.Data;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repositories;

public class Repo<T>(DataContext db) : IRepo<T> where T : class
{
    public DbSet<T> Table { get; } = db.Set<T>();
    
    public IEnumerable<T> GetAll()
    {
        return Table.ToList();
    }
    public T GetById(object id)
    {
        return Table.Find(id);
    }

    public void Insert(T obj)
    {
        Table.Add(obj);
    }
    public void Update(T obj)
    {
        Table.Attach(obj);
        db.Entry(obj).State = EntityState.Modified;
    }

    public void Delete(object id)
    {
        T existing = Table.Find(id);
        Table.Remove(existing);
    }
    
    public void Save()
    {
        db.SaveChanges();
    }
}