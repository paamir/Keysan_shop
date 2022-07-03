using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;using Microsoft.EntityFrameworkCore.Query;

namespace _0_Framework.Domain
{
    public interface IRepository<TKey, T>
    {
        void Create(T entity);
        T GetBy(TKey id);
        void SaveChanges();
        bool Exists(Expression<Func<T, bool>> expression);
    }
}
