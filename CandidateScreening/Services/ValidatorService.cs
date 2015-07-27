using System;
using CandidateScreening.Services.Interfaces;

namespace CandidateScreening.Services
{
    public class ValidatorService:IValidatorService
    {
        public bool Validate<T>(T entity,Func<T, bool> func)
        {
            return func(entity);
        }
    }
}