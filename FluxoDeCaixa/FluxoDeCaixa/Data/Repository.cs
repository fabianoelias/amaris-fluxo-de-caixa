using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        private DbSet<T> dbSet;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            db = applicationDbContext;
            dbSet = db.Set<T>();
        }

        public DbSet<T> Get
        {
            get { return dbSet; }
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            db.SaveChanges();
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
            db.SaveChanges();
        }

        public T? FindById(int? Id)
        {
            return dbSet.Find(Id);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}