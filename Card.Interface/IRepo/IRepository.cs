using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.IRepo
{
   public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(object id);
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
        IQueryable<T> Table { get; }
        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);
        IEnumerable<T> ExecWithStoreProcedure(string querys);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void DisposeTransaction();
    }
}
