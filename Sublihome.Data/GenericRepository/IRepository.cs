using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sublihome.Data.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task<T> GetAsync(int id);

        IQueryable<T> GetAll();

        void Update(T entity);

        void Delete(T entity);

        Task SaveAsync();

        void Save();
    }
}
