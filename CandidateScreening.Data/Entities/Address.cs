using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateScreening.Data.Entities
{
    public class Address
    {
        public int Id { set; get; }
        public string StreetNumber { set; get; }
        public string Line1 { set; get; }
        public string Line2 { set; get; }
        public string PostCode { set; get; }
        public string Suburb { set; get; }
        public string Country { set; get; }
        public bool DefaultAddress { set; get; }
        public AddressType AddressType { set; get; }
    }

    public enum AddressType
    {
        Residential = 0,
        Postal = 1,
    }
}
