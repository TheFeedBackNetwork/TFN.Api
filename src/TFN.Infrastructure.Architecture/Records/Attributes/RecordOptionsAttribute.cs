using System;

namespace TFN.Infrastructure.Architecture.Records.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class RecordOptionsAttribute : Attribute
    {
        public string RecordTableName { get; }

        public RecordOptionsAttribute(string recordTableName)
        {
            if (String.IsNullOrWhiteSpace(recordTableName))
            {
                throw new ArgumentNullException(nameof(recordTableName), "Collection name cannot be null or empty");
            }

            RecordTableName = recordTableName;
        }
    }
}