using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CandidateScreening.Data.Entities;

namespace CandidateScreening.Data.Respositories.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T GetEntity(int id);
        IQueryable<T> GetAll();
        T AddOrUpdate(T t);
        IQueryable<Patient> Search(Expression<Func<T, bool>> predicate);

    }
}
