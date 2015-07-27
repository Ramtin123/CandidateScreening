using System;
using System.Linq;
using CandidateScreening.Data.Base;
using CandidateScreening.Data.Entities;
using CandidateScreening.Data.Respositories.Interfaces;
using CandidateScreening.Exceptions;
using CandidateScreening.Services;
using CandidateScreening.Services.Interfaces;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

//We definitely need more unit tests for this application . All modules in the patient repository , UnitOfWork , Controller need testing. We also need some unit tests for javascrip codes. Even for PatientService we need to cover more senarios . But because this application is only for testing I did not finish all unit tests.

namespace CandidateScreening.Tests.Services
{
    [TestFixture]
    public class PatientServiceTest
    {
        [SetUp]
        public void Init()
        {
            patient = new Patient
            {
                Firstname = "AA",
                Surname = "BB",
                DateOfBirth = DateTime.Today,
                Gender = "M",
                Index = 1
            };
            residentialAddress = new Address
            {
                StreetNumber = "10",
                Line1 = "Hope st",
                Suburb = "ASD Park",
                PostCode = "4000",
                Country = "Australia",
                DefaultAddress = true,
                AddressType = AddressType.Residential
            };
            postalAddress = new Address
            {
                StreetNumber = "20",
                Line1 = "Tom st",
                Suburb = "Norman Park",
                PostCode = "4002",
                Country = "Australia",
                DefaultAddress = false,
                AddressType = AddressType.Postal
            };
            patient.Addresses.Add(residentialAddress);
            patient.Addresses.Add(postalAddress);
        }

        private readonly IPatientService patientService;
        private readonly IPatientRepository patientRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidatorService validatorService;
        private Patient patient;
        private Address residentialAddress;
        private Address postalAddress;

        public PatientServiceTest()
        {
            patientRepository = Substitute.For<IPatientRepository>();
            unitOfWork = Substitute.For<IUnitOfWork>();
            validatorService = Substitute.For<IValidatorService>();
            patientService = new PatientService(patientRepository, unitOfWork, validatorService);
        }

        [Test]
        public void ItCanAddPatientSuccesfuly()
        {
            validatorService.Validate(Arg.Is(patient), Arg.Any<Func<Patient, bool>>()).Returns(true);
            patientService.AddPatient(patient);
            patientRepository.Received().AddOrUpdate(Arg.Is(patient));
            unitOfWork.Received().Save();
        }

        [Test]
        [ExpectedException(typeof (ValidationException))]
        public void ItCanNotAddAnInvalidPatient()
        {
            validatorService.Validate(Arg.Is(patient), Arg.Any<Func<Patient, bool>>()).Returns(false);
            patientService.AddPatient(patient);
            patientRepository.DidNotReceive().AddOrUpdate(Arg.Is(patient));
            unitOfWork.DidNotReceive().Received().Save();
        }

        [Test]
        [ExpectedException(typeof (ValidationException))]
        public void ItCanNotAddAPatientWithTwoDefaultAddresses()
        {
            postalAddress.DefaultAddress = true; //we have two default addresses now 
            validatorService.Validate(Arg.Is(patient), Arg.Any<Func<Patient, bool>>()).Returns(true);
            patientService.AddPatient(patient);
            patientRepository.DidNotReceive().AddOrUpdate(Arg.Is(patient));
            unitOfWork.DidNotReceive().Received().Save();
        }

        [Test]
        [ExpectedException(typeof (ValidationException))]
        public void ItShouldNotBePossibleToAddTwoAddressWithOneType()
        {
            var newResidentialAddress = new Address
            {
                StreetNumber = "17",
                PostCode = "4646",
                DefaultAddress = false,
                Line1 = "Something",
                Suburb = "ww Park",
                Country = "ter"
            };
            patient.Addresses.Add(newResidentialAddress);
            validatorService.Validate(Arg.Is(patient), Arg.Any<Func<Patient, bool>>()).Returns(true);
            patientService.AddPatient(patient);
            patientRepository.DidNotReceive().AddOrUpdate(Arg.Is(patient));
            unitOfWork.DidNotReceive().Received().Save();
        }

        [Test]
        public void ItWillAddAnyAddressWhichMissedThatTypeInRepositoryWhenUpdating()
        {
            var patinetInRepository = new Patient
            {
                Firstname = "AAA",
                Surname = "BBB",
                DateOfBirth = DateTime.Today,
                Gender = "M",
                Index = 1
            };
            var residentialAddressInRepository = new Address
            {
                StreetNumber = "10",
                Suburb = "dfgdf",
                PostCode = "4544",
                Line1 = "Irwe rwer",
                Country = "ewrwe",
                DefaultAddress = true,
                AddressType = AddressType.Residential
            };
            validatorService.Validate(Arg.Is(patient), Arg.Any<Func<Patient, bool>>()).Returns(true);
            patinetInRepository.Addresses.Add(residentialAddressInRepository);
            patientRepository.GetPatientWithAddress(Arg.Any<int>()).Returns(patinetInRepository);
            patientService.UpdatePatient(patient);
            patientRepository.Received().AddOrUpdate(Arg.Any<Patient>());
            unitOfWork.Received().Save();
            patient.Addresses.Any(a => a.AddressType == AddressType.Postal).Should().BeTrue();
        }
    }
}