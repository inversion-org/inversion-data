using System.Threading;

namespace Inversion.Data
{
    public abstract class SyncBaseStore : Store
    {
        private readonly ReaderWriterLockSlim _lockSlim = new ReaderWriterLockSlim();

        protected ReaderWriterLockSlim LockSlim { get { return _lockSlim; } }
    }
}