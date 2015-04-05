using System;
using System.Runtime.Serialization;

namespace Inversion.Data
{
    [Serializable]
    public sealed class StoreProcessException : ApplicationException
    {
        public StoreProcessException(string message)
            : base(message) { }

        public StoreProcessException(string message, Exception innerException)
            : base(message, innerException) { }

        private StoreProcessException(
            SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}