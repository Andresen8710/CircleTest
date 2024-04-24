using PruebaTecnicaCycle.Domain.Dtos.Identity;

namespace PruebaTecnicaCycle.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> RegisterUser(RegistrationRequest request);
    }
}