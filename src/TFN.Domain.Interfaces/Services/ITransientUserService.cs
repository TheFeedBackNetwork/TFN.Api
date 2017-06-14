using System.Threading.Tasks;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface ITransientUserService
    {
        Task CreateAsync(TransientUserAccount transientUserAccount);
        Task<bool> EmailVerificationKeyExistsAsync(string emailVerificationKey);
        Task DeleteAsync(TransientUserAccount transientUserAccount);
        Task<TransientUserAccount> GetByEmailVerificationKeyAsync(string emailVerificationKey);
        Task<TransientUserAccount> GetByEmailAsync(string email);
        Task<TransientUserAccount> GetByUsernameAsync(string username);
    }
}