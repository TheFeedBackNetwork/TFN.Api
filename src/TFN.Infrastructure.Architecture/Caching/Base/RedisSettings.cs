namespace TFN.Infrastructure.Architecture.Caching.Base
{
    public class RedisSettings
    {
        public string Endpoint { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public bool Ssl { get; set; }
    }
}