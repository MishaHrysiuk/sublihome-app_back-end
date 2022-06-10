using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sublihome.Data.ContextDb;

namespace Sublihome.Data.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SublihomeDbContext _sublihomeDbContext;

        public Repository(SublihomeDbContext sublihomeDbContext)
        {
            _sublihomeDbContext = sublihomeDbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _sublihomeDbContext.Set<T>().AddAsync(entity);
            await SaveAsync();
        }

        public void Update(T entity)
        {
            _sublihomeDbContext.Set<T>().Update(entity);
            Save();
        }

        public void Delete(T entity)
        {
            _sublihomeDbContext.Set<T>().Remove(entity);
            Save();
        }

        public IQueryable<T> GetAll()
        {
            return _sublihomeDbContext.Set<T>();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _sublihomeDbContext.Set<T>().FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _sublihomeDbContext.SaveChangesAsync();
        }

        public void Save()
        {
            _sublihomeDbContext.SaveChanges();
        }
    }
}
