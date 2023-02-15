using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Data
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Get { get; }
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T? FindById(int? Id);
    }
}