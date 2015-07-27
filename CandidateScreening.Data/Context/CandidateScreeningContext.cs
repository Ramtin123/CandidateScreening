using System.Data.Entity;
using CandidateScreening.Data.Base;
using CandidateScreening.Data.DataLayerMappings;
using CandidateScreening.Data.Entities;

namespace CandidateScreening.Data.Context
{
    public interface ICandidateScreeningContext : IContext
    {
        IDbSet<Patient> Patients { get; set; }
    }

    public class CandidateScreeningContext : DbContext, ICandidateScreeningContext
    {
        public IDbSet<Patient> Patients { get; set; }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void SetAdd(object entity)
        {
            Entry(entity).State = EntityState.Added;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = false;
            modelBuilder.Configurations.Add(new PatientMap());
        }
        
    }
}