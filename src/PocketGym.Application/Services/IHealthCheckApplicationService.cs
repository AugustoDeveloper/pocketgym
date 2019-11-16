namespace PocketGym.Application.Services
{
    public interface IHealthCheckApplicationService
    {
        bool Check();
        string GetDetails();
    }
}