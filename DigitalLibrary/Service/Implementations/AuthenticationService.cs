using Domain.Auth;
using Domain.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;

namespace Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthenticationService(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = new ApplicationUser
            {
                FirstName = userForRegistration.FirstName,
                LastName = userForRegistration.LastName,
                UserName = userForRegistration.UserName,
                Email = userForRegistration.Email
            };

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            return result;
        }
    }
}
