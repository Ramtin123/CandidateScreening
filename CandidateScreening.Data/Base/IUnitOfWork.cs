using System;

namespace CandidateScreening.Data.Base
{
  public interface IUnitOfWork:IDisposable 
  {
    int Save();
    IContext Context { get; }
  
  }
  
}