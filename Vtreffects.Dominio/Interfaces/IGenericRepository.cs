using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //Async Methods
        Task<IEnumerable<T>> AllAsync();
        Task<T> SaveAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);



        //Async Methods de Pesquisa

        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();

        Task<List<T>> Get(
                      Expression<Func<T, bool>>? filter = null,
                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                      params Expression<Func<T, object>>[] includes);


        Task<ICollection<T>> GetAsyn(Expression<Func<T, bool>> predicate);

        Task<IQueryable<T>> Query(Expression<Func<T, bool>>? filter = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        T? GetById(int id);

        Task<T?> FindAsync(Expression<Func<T, bool>> entity);


        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);


    }


}
