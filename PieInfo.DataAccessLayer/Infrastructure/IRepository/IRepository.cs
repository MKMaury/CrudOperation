using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PieInfo.DataAccessLayer.Infrastructure.IRepository
{
    public interface IRepository <T> where T : class
    {
        IEnumerable<T> GetAll (string ?includePropeties=null);
        T GetT(Expression<Func<T, bool>> predicate, string? includePropeties = null);
        void Add(T entity);
        void Delete(T entity);  
        void DeleteRange(IEnumerable<T> entity);  
    }
}
