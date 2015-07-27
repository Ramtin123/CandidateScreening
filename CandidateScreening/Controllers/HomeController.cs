using System.Web.Mvc;
using CandidateScreening.Data.Base;
using CandidateScreening.Models;
using CandidateScreening.Services.Interfaces;

namespace CandidateScreening.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IPatientService patientService, IUnitOfWork unitOfWork)
        {
            _patientService = patientService;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var model = new IndexModel();
            //var result = _patientService.GetAllPatients().ToList();
            ////Patient patient = new Patient
            ////{
            ////    Firstname = "Ramtin",
            ////    Surname = "Arab",
            ////    DateOfBirth = DateTime.Parse("1977-02-05"),
            ////    Gender = "M",
            ////    Email="ramtin@yahoo.com",
            ////    Index=102,
            ////    Created = DateTime.Now
            ////};
            ////patient.Addresses.Add(new Address
            ////{
            ////    StreetNumber = "10",
            ////    AddressType = AddressType.Home,
            ////    Line1 = "Rawnsley",
            ////    PostCode = "4102",
            ////    Country = "Australia"
            ////});
            ////_patientRepository.AddOrUpdate(patient);
            ////_unitOfWork.Save();

            return View(model);
        }
    }
}