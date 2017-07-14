using System.Threading.Tasks;
using IdentityServer4.Validation;
using TFN.Domain.Interfaces.Services;

namespace TFN.Domain.Services.IdentityServer.Validators
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public IUserService UserService { get; private set; }
        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            UserService = userService;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await UserService.Find(context.UserName, context.Password);

            if (user != null)
            {
                var claims = user.GetClaims();
                context.Result = new GrantValidationResult(user.Id.ToString(), "password",claims);
            }
        }
    }
}
