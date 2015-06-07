using System.Threading;

namespace Inversion.Data
{
    public abstract class SyncBaseStore : StoreBase
    {
        private readonly ReaderWriterLockSlim _lockSlim = new ReaderWriterLockSlim();

        protected ReaderWriterLockSlim LockSlim { get { return _lockSlim; } }
    }
}