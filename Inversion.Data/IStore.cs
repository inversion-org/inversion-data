using System;

namespace Inversion.Data
{
    public interface IStore : IDisposable
    {
        bool HasStarted { get; }

        void Start();
        void Stop();
    }
}