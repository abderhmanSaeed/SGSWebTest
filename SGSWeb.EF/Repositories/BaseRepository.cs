using Microsoft.EntityFrameworkCore;
using SGSWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SGSWeb.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public T GetById(int? EmployeeId, string[] includes = null)
        {
            return _context.Set<T>().Find(EmployeeId);
        }
        public IEnumerable<T> GetAll(string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query.ToList();
        }




        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(criteria).ToList();
        }



        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.SingleOrDefault(criteria);
        }

        IEnumerable<T> IBaseRepository<T>.GetAll()
        {
            return _context.Set<T>().ToList();
        }


        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);

            return entity;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }



        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> whereCondition, string[] includes = null)
        {
            var dbResult = _context.Set<T>().Where(whereCondition).FirstOrDefault();
            return dbResult;
        }
    }
}
