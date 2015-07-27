using System.Data.Entity;

namespace CandidateScreening.Data.Base
{
  public class BaseContext<TContext> 
    : DbContext where TContext : DbContext
  {
    static BaseContext()
    {
      Database.SetInitializer<TContext>(null);
    }
    protected BaseContext()
        : base("name=CandidateScreeningContext")
    {}
  }
 }
