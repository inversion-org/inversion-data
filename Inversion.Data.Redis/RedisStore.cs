using System;
using StackExchange.Redis;

namespace Inversion.Data.Redis
{
    public class RedisStore : Store
    {
        private readonly string _connections;

        protected ConnectionMultiplexer ConnectionMultiplexer { get; private set; }

        private readonly bool _oneUse;
        private bool _disposed;

        public RedisStore(string connections)
        {
            _connections = connections;
            _oneUse = true;
        }

        public RedisStore(ConnectionMultiplexer connectionMultiplexer)
        {
            this.ConnectionMultiplexer = connectionMultiplexer;
            _oneUse = false;
        }

        public override void Start()
        {
            base.Start();

            if (this.ConnectionMultiplexer != null)
            {
                this.ConnectionMultiplexer = ConnectionMultiplexer.Connect(_connections);
            }
        }

        public override void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            if (_oneUse)
            {
                this.ConnectionMultiplexer.Dispose();
            }
        }
    }
}