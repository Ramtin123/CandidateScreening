using CandidateScreening.Data.Context;

namespace CandidateScreening.Data.Migrations
{
	using CandidateScreening.Data.Entities;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	public sealed class Configuration : DbMigrationsConfiguration<CandidateScreeningContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(CandidateScreeningContext context)
		{
            if (context.Patients.Any()) return;
			// Lots of patients, should probably keep that in mind on the patient list
			for (var i = 0; i < 1000; i++)
			{
				var isMale = Faker.BooleanFaker.Boolean();

				var patient = new Patient
				{
					Firstname = isMale ? Faker.NameFaker.MaleFirstName() : Faker.NameFaker.FemaleFirstName(),
					Surname = Faker.NameFaker.LastName(),
					Gender = isMale ? "M" : "F",
					DateOfBirth = Faker.DateTimeFaker.DateTimeBetweenYears(95),
					Email = string.Format("{0}@{1}.com.au", Faker.StringFaker.AlphaNumeric(5), Faker.StringFaker.AlphaNumeric(10)),
					Index = i,
					Created = DateTime.Now
				};

				context.Patients.AddOrUpdate(p => p.Index, patient);
			}
			context.SaveChanges();
		}
	}
}
