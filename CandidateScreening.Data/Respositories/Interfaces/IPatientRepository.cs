using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateScreening.Data.Entities;

namespace CandidateScreening.Data.Respositories.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
         Patient GetPatientWithAddress(int id);
    }
}
