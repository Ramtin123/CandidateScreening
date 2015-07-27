using System;

namespace CandidateScreening.Data.Base
{
  public interface IContext : IDisposable
  {
    int SaveChanges();
    void SetModified(object entity);
    void SetAdd(object entity);
  }
}
