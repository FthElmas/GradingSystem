using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingSystem.DAL.Abstract.Interface
{
    public interface IGenericRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool Add(T entity);
        T ReturnAdd(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IEnumerable<T> GetAll(string columnName, Guid ID);
        IEnumerable<T> GetAll(string columnName, int ID);
    }
}
