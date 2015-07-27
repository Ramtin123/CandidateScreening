using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CandidateScreening.Data.Base;
using CandidateScreening.Data.Context;
using CandidateScreening.Data.Entities;
using CandidateScreening.Data.Respositories.Interfaces;

namespace CandidateScreening.Data.Respositories
{
    public class PatientRepository:IPatientRepository
    {
        public PatientRepository(IUnitOfWork uow)
        {
            _context = uow.Context as ICandidateScreeningContext;
        }
        private readonly ICandidateScreeningContext _context;
        public Patient GetEntity(int id)
        {
            return _context.Patients
                           .SingleOrDefault(r => r.Id == id);
        }

        public IQueryable<Patient> GetAll()
        {
            return _context.Patients;
        }

        
        public Patient AddOrUpdate(Patient patient)
        {
            if (patient.Id == default(int)) // New entity
            {
                _context.SetAdd(patient);
            }
            else        // Existing entity
            {
                _context.SetModified(patient);
            };
            foreach (var address in patient.Addresses)
            {
                if (address.Id == default(int))
                {
                    _context.SetAdd(address);
                }
                else
                {
                    _context.SetModified(address);
                }
            }
            return patient;
        }

        public IQueryable<Patient> Search(Expression<Func<Patient, bool>> predicate)
        {
            return _context.Patients.Where(predicate);
        }

        public Patient GetPatientWithAddress(int id)
        {
            return _context.Patients.Where(w=>w.Id==id).Include(i=>i.Addresses).FirstOrDefault();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
