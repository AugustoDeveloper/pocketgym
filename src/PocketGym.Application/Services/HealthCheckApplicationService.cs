using System;
using PocketGym.Application.Core.Dtos;
using PocketGym.Domain.Repositories;

namespace PocketGym.Application.Services
{
    public class HealthCheckApplicationService : IHealthCheckApplicationService
    {
        private IUserRepository repository;
        public HealthCheckApplicationService(IUserRepository repo)
        {
            repository = repo;
        }

        public bool Check()
        {
            try
            {
                repository.TestConnection();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public HealthDto GetDetails()
            => new HealthDto
            {
                Success = Check(),
                Details = new string[] 
                {
                    Check() ? "Databse is OK" : "Database has failed"
                }
            };
    }
}