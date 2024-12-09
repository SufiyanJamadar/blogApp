
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace blog_backend.Data
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext dbcontext;
        public SqlRepository(AppDbContext dbContext) 
        {
            this.dbcontext=dbContext;
        }
        public async Task AddAsync(T entity)
        {
           await  dbcontext.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await dbcontext.Set<T>().FindAsync(id);
            dbcontext.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAll()
        {
          return await dbcontext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            return await dbcontext.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
          return await dbcontext.Set<T>().FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
           await dbcontext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
           dbcontext.Set<T>().Update(entity);
        }
    }
}
