using Autofac;
using Autofac.Integration.Mvc;
using CandidateScreening.Data.Base;
using CandidateScreening.Data.Context;
using CandidateScreening.Data.Respositories;
using CandidateScreening.Data.Respositories.Interfaces;
using CandidateScreening.Services;
using CandidateScreening.Services.Interfaces;
using Serilog;

namespace CandidateScreening.AutofacModules
{
    public class CandidateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterControllers(ThisAssembly);
            builder.RegisterType<CandidateScreeningContext>().As<ICandidateScreeningContext>().SingleInstance();
            builder.RegisterType<UnitOfWork<CandidateScreeningContext>>()
                .As<IUnitOfWork>()
                .SingleInstance();

            builder.RegisterType<PatientRepository>().As<IPatientRepository>().SingleInstance();
            builder.RegisterType<ValidatorService>()
                .As<IValidatorService>();
            builder.RegisterType<PatientService>().As<IPatientService>();

            builder.Register(r =>
            {
                var logger = new LoggerConfiguration().CreateLogger();
                return logger;
            }).As<ILogger>();
        }
    }
}