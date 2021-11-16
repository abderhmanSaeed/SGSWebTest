using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SGSWeb.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int? EmployeeId, string[] includes = null);

        /// <summary>
        /// Returns all the rows for type T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Returns all the rows for type T on includes
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll(string[] includes = null);

        /// <summary>
        /// Returns all the rows for type T on basis of filter condition
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);

        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);


        /// <summary>
        /// Inserts the data into the table
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="userId">The user performing the insert</param>
        /// <returns></returns>
        T Add(T entity);

        /// <summary>
        /// Updates this entity in the database using it's primary key
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="userId">The user performing the update</param>

        T Update(T entity);

        /// <summary>
        /// Deletes this entry fro the database
        /// ** WARNING - Most items should be marked inactive and Updated, not deleted
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="userId">The user Id who deleted the entity</param>
        /// <returns></returns>
        void Delete(T entity);


        void DeleteRange(IEnumerable<T> entities);


        /// <summary>
        /// Retrieve a single item by it's primary key or return null if not found
        /// </summary>
        /// <param name="primaryKey">Prmary key to find</param>
        /// <returns>T</returns>
        T SingleOrDefault(Expression<Func<T, bool>> whereCondition, string[] includes = null);
    }
}
