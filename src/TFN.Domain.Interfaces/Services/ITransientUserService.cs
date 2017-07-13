using System.Threading.Tasks;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface ITransientUserService
    {
        Task Create(TransientUserAccount transientUserAccount);
        Task<bool> EmailVerificationKeyExists(string emailVerificationKey);
        Task Delete(TransientUserAccount transientUserAccount);
        Task<TransientUserAccount> FindByEmailVerificationKey(string emailVerificationKey);
        Task<TransientUserAccount> FindByEmail(string email);
        Task<TransientUserAccount> FindByUsername(string username);
    }
}