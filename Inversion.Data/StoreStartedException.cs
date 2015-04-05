using System;
using System.Runtime.Serialization;

namespace Inversion.Data
{
    [Serializable]
    public sealed class StoreStartedException : ApplicationException
    {
        public StoreStartedException(string message)
            : base(message) { }

        public StoreStartedException(string message, Exception innerException)
            : base(message, innerException) { }

        private StoreStartedException(
            SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}