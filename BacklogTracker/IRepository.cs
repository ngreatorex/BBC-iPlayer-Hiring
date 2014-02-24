using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker
{
    public interface IRepository<T, in TKey> where T : class
    {
        void Insert(TKey id, T entity);
        void Update(TKey id, T entity);
        T DeleteById(TKey id);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(TKey id);
    }
}
