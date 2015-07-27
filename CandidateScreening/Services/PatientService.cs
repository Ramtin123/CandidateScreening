using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Configuration;
using CandidateScreening.Data.Base;
using CandidateScreening.Data.Entities;
using CandidateScreening.Data.Respositories.Interfaces;
using CandidateScreening.Exceptions;
using CandidateScreening.Services.Interfaces;

namespace CandidateScreening.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly Func<Patient, bool> _patientValidatorFunc;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorService _validatorService;

        public PatientService(IPatientRepository patientRepository, IUnitOfWork unitOfWork,
            IValidatorService validatorService)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _validatorService = validatorService;
            _patientValidatorFunc = p => !string.IsNullOrEmpty(p.Firstname) && !string.IsNullOrEmpty(p.Surname);
        }

        private void ValidatePatientAggregate(Patient patient)
        {
            if (!_validatorService.Validate(patient, _patientValidatorFunc))
                throw new ValidationException("Null or empity information for patient");
            if (patient.Addresses.Count(w => w.DefaultAddress) > 1)
                throw new ValidationException("More than one default address is invalid");
            if (patient.Addresses.Count(w => w.AddressType == AddressType.Postal) > 1 ||
                patient.Addresses.Count(w => w.AddressType == AddressType.Residential) > 1)
                throw new ValidationException("There can be only one Postal or Residential address for any patient");
        }

        public void AddPatient(Patient patient)
        {
            ValidatePatientAggregate(patient);
            patient.Created=DateTime.Now;
            _patientRepository.AddOrUpdate(patient);
            _unitOfWork.Save();
        }

        public void UpdatePatient(Patient patient)
        {
            ValidatePatientAggregate(patient);
            var aggregate = _patientRepository.GetPatientWithAddress(patient.Id);
            if (aggregate == null) throw new ValidationException("Patient not found");
            aggregate.Firstname = patient.Firstname;
            aggregate.Surname = patient.Surname;
            aggregate.DateOfBirth = patient.DateOfBirth;
            aggregate.Email = patient.Email;
            aggregate.Gender = patient.Gender;
            foreach (var address in patient.Addresses)
            {
                var oldAddress = aggregate.Addresses.FirstOrDefault(f => f.AddressType == address.AddressType);
                if(oldAddress==null)
                    aggregate.Addresses.Add(address);
                else
                {
                    oldAddress.StreetNumber = address.StreetNumber;
                    oldAddress.Country = address.Country;
                    oldAddress.Line1 = address.Line1;
                    oldAddress.Line2 = address.Line2;
                    oldAddress.PostCode = address.PostCode;
                    oldAddress.Suburb = address.Suburb;
                    oldAddress.DefaultAddress = address.DefaultAddress;
                }
            }
                _patientRepository.AddOrUpdate(aggregate);
                _unitOfWork.Save();
        }

        public Patient GetPatientDetails(int patientId)
        {
            Patient patient = _patientRepository.GetPatientWithAddress(patientId);
            if (patient == null) throw new ValidationException("Patient not found");

            return patient;
        }

        public IQueryable<Patient> GetAllPatients()
        {
            return _patientRepository.GetAll();
        }

        public Task<IQueryable<Patient>> GetAllPatientsAsync()
        {
            return Task.Run(() => _patientRepository.GetAll());
        }

        public IQueryable<Patient> SearchPatients(Expression<Func<Patient, bool>> predicate)
        {
            return _patientRepository.Search(predicate);
        }

        public Task<IQueryable<Patient>> SearchPatientsAsync(Expression<Func<Patient, bool>> predicate)
        {
            return Task.Run(() => _patientRepository.Search(predicate));
        }

    }
}