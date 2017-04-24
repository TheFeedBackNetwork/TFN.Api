using System;

namespace TFN.Infrastructure.Architecture.Documents.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class CollectionOptionsAttribute : Attribute
    {
        public string CollectionName { get; }

        public CollectionOptionsAttribute(string collectionName)
        {
            if (String.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName), "Collection name cannot be null or empty");
            }

            CollectionName = collectionName;
        }
    }
}