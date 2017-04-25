using TFN.Domain.Interfaces.Services;

namespace TFN.Domain.Services.Cryptography
{
    public class KeyService : IKeyService
    {
        public string GenerateUrlSafeUniqueKey()
        {
            return Cryptography.CreateUrlSafeUniqueId(22);
        }
    }
}