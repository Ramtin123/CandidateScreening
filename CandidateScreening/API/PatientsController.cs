using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CandidateScreening.Data.Base;
using CandidateScreening.Data.Entities;
using CandidateScreening.Exceptions;
using CandidateScreening.Services.Interfaces;
using Serilog;

namespace CandidateScreening.API
{
    public class PatientsController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IPatientService _patientService;
        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(IPatientService patientService, IUnitOfWork unitOfWork, ILogger logger)
        {
            _patientService = patientService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public HttpResponseMessage Get(string page, string pageSize, string searchString = "")
        {
            try
            {
                IQueryable<Patient> patients;
                if (string.IsNullOrWhiteSpace(searchString))
                {
                    patients = _patientService.GetAllPatients();
                }
                else
                {
                    patients =
                        _patientService.SearchPatients(s => s.Firstname.ToLower().Contains(searchString.ToLower())
                                                            || s.Surname.ToLower().Contains(searchString.ToLower())
                                                            || s.Id.ToString().Equals(searchString));
                }

                var totalItems = patients.Count();
                int nPage;
                int nPageSize;
                if (int.TryParse(page, out nPage) && int.TryParse(pageSize, out nPageSize))
                {
                    if (totalItems >= nPageSize*nPage)
                    {
                        patients = patients.OrderByDescending(o => o.Id)
                            .Skip(nPageSize * (nPage - 1)).Take(nPageSize);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    List = patients.ToList(),
                    TotalItems = totalItems
                }
                    );
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Internal functionality exception {@Exception}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Get(int patientId)
        {
            try
            {
                var patient = _patientService.GetPatientDetails(patientId);
                return Request.CreateResponse(HttpStatusCode.OK, patient);
            }
            catch (ValidationException ex)
            {
                _logger.Error(ex, "Validation exception {@Exception}", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Internal functionality exception {@Exception}", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Post([FromBody]Patient patient)
        {
            try
            {
                _patientService.AddPatient(patient);
                return Request.CreateResponse(HttpStatusCode.OK,
                                  patient);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Internal functionality exception {@Exception}", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage Put([FromBody]Patient patient)
        {
            try
            {
                _patientService.UpdatePatient(patient);
                return Request.CreateResponse(HttpStatusCode.OK,
                                  patient);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Internal functionality exception {@Exception}", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}