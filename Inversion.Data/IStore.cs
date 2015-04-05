using System;

namespace Inversion.Data
{
    public interface IStore : IDisposable
    {
        bool HasStarted { get; }
        bool HasStopped { get; }

        void Start();
        void Stop();
    }
}