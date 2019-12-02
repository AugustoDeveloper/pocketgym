using PocketGym.Application.Core.Dtos;

namespace PocketGym.Application.Services
{
    public interface IHealthCheckApplicationService
    {
        bool Check();
        HealthDto GetDetails();
    }
}