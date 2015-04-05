using System;
using System.Runtime.Serialization;

namespace Inversion.Data
{
    [Serializable]
    public sealed class StoreStoppedException : ApplicationException
    {
        public StoreStoppedException(string message)
            : base(message) { }

        public StoreStoppedException(string message, Exception innerException)
            : base(message, innerException) { }

        private StoreStoppedException(
            SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}