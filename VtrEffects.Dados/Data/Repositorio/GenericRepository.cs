using Microsoft.EntityFrameworkCore;
using VtrEffects.Dominio;
using VtrEffects.Dominio.Interfaces;
using VtrEffectsDados.Data.Context;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VtrEffectsDados.Data.Repositorio
{

    
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ContextVTR context;
        protected DbSet<T> entity_;
        protected ContextVTR contextVTR;

   

        public GenericRepository(ContextVTR contextVTR)
        {
            this.context = contextVTR;
            this.entity_ = context.Set<T>();

        }
        public IEnumerable<T> GetAll()
        {
            return entity_.ToList();
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            return await entity_.ToListAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            entity_.Remove(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async  Task<T?> FindAsync(Expression<Func<T, bool>> entity)
        {
            return await entity_.SingleOrDefaultAsync(entity);
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = entity_;
            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await entity_.ToListAsync();
        }



        public T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public Task<IQueryable<T>> Query(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = entity_;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return (Task<IQueryable<T>>)query;
        }

        public async Task<T> SaveAsync(T entity)
        {
            entity_.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity_.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }


        public async Task<ICollection<T>> GetAsyn(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
                //(Task<IQueryable<T>>)entity_.Where(predicate).AsQueryable();
        }


    }
}
