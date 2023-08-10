using Domain.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
    }
}
