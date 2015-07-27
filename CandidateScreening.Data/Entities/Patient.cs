using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateScreening.Data.Entities
{
    public class Patient
    {
        public Patient()
        {
            this.Addresses=new List<Address>();
        }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } //M or F
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public int Index { get; set; } //this is only used to make sure we just seed 1000 patients  --- I do not like it . I think it has a violation of single resposibility . Ramtin
        public virtual ICollection<Address> Addresses { set; get; }

    }
}
