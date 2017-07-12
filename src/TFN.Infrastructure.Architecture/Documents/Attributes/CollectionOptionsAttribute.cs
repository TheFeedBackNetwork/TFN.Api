using System;

namespace TFN.Infrastructure.Architecture.Documents.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class CollectionOptionsAttribute : Attribute
    {
        public string CollectionName { get; }
        public string TypeName { get; }

        public CollectionOptionsAttribute(string collectionName, string typeName)
        {
            if (String.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName), "Collection name cannot be null or empty");
            }

            if (String.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException(nameof(typeName), "Type name cannot be null or empty");
            }

            CollectionName = collectionName;
            TypeName = typeName;
        }
    }
}