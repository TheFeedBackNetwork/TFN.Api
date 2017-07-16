using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TFN.Api.Authorization.Models.Resource;

namespace TFN.Api.Authorization.Handlers
{
    public class LikeAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, LikeAuthorizationModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
            LikeAuthorizationModel resource)
        {
            var noOp = Task.CompletedTask;

            if (requirement.Name == "LikeWrite")
            {
                if (context.User.HasClaim("sub", resource.PostOwnerId.ToString()))
                {
                    context.Fail();
                    return noOp;
                }

                if (context.User.HasClaim("sub", resource.OwnerId.ToString()))
                {
                    context.Succeed(requirement);
                    return noOp;
                }

            }

            if (requirement.Name == "LikeDelete")
            {
                if (context.User.HasClaim("sub", resource.PostOwnerId.ToString()))
                {
                    context.Fail();
                    return noOp;
                }

                if (context.User.HasClaim("sub", resource.OwnerId.ToString()))
                {
                    context.Succeed(requirement);
                    return noOp;
                }
            }

            context.Succeed(requirement);
            return noOp;
        }
    }
}