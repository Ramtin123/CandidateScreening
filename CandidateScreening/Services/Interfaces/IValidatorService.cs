using System;

namespace CandidateScreening.Services.Interfaces
{
    public interface IValidatorService
    {
        bool Validate<T>(T entity,Func<T, bool> func);
    }
}