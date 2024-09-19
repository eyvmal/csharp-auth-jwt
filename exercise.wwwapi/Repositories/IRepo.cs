using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repositories;

public interface IRepo<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(object id);
    void Insert(T obj);
    void Update(T obj);
    void Delete(object id);
    void Save();
    DbSet<T> Table { get; }
}