using System;

namespace MHA.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CollectionNameAttribute : Attribute
    {
        public string CollectionName { get; }

        public CollectionNameAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
