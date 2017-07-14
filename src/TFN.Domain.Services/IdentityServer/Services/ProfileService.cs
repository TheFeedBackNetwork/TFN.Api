using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Domain.Services.IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        public IUserAccountRepository UserAccountRepository { get; private set; }

        public ProfileService(IUserAccountRepository userAccountRepository)
        {
            UserAccountRepository = userAccountRepository;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            Guid subjectId;

            var validSubject = Guid.TryParse(context.Subject.GetSubjectId(), out subjectId);

            if (!validSubject)
            {
                throw new ArgumentException($"{nameof(context.Subject)} is invalid as a subject Id.");
            }

            var user = await UserAccountRepository.Find(subjectId);

            var claims = new List<Claim>();

            claims.AddRange(user.GetClaims());

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            Guid subjectId;

            var validSubject = Guid.TryParse(context.Subject.GetSubjectId(), out subjectId);

            if (!validSubject)
            {
                throw new ArgumentException($"{nameof(context.Subject)} is invalid as a subject Id.");
            }

            var user = await UserAccountRepository.Find(subjectId);

            context.IsActive = (user != null) && user.IsActive;
        }
    }
}
