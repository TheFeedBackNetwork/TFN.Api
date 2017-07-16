using System;

namespace TFN.Domain.Architecture.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class CacheVersionAttribute : Attribute
    {
        public int Version { get; }

        public CacheVersionAttribute(int version)
        {
            if (version < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(version), $"Cache version number cannot be negative. Value is {version}.");
            }

            Version = version;
        }
    }
}