using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TFN.Api.Authorization.Operations
{
    public class LikeOperations
    {
        public static OperationAuthorizationRequirement Write = new OperationAuthorizationRequirement { Name = "LikeWrite" };
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = "LikeDelete" };
    }
}