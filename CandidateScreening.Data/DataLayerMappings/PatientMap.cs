using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CandidateScreening.Data.Entities;

namespace CandidateScreening.Data.DataLayerMappings
{
    internal class PatientMap : EntityTypeConfiguration<Patient>
    {
        public PatientMap()
        {
            this.HasMany(contact => contact.Addresses)
                .WithOptional()
                .Map(map => map.MapKey("PatientId"));
        }
        
        
    }
}
