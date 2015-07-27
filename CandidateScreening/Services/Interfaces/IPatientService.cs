using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using CandidateScreening.Data.Entities;

namespace CandidateScreening.Services.Interfaces
{
    public interface IPatientService
    {
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);
        Patient GetPatientDetails(int patientId);
        IQueryable<Patient> GetAllPatients();
        Task<IQueryable<Patient>>  GetAllPatientsAsync();
        IQueryable<Patient> SearchPatients(Expression<Func<Patient, bool>> predicate);
        Task<IQueryable<Patient>> SearchPatientsAsync(Expression<Func<Patient, bool>> predicate);
    }
}