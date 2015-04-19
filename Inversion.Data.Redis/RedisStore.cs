using System;
using StackExchange.Redis;

namespace Inversion.Data.Redis
{
    public class RedisStore : Store
    {
        private readonly string _connections;

        protected ConnectionMultiplexer ConnectionMultiplexer { get; private set; }

        protected IDatabase Database { get; private set; }

        private readonly int _databaseNumber;

        private readonly bool _oneUse;
        private bool _disposed;

        public RedisStore(string connections, int databaseNumber)
        {
            _connections = connections;
            _databaseNumber = databaseNumber;
            _oneUse = true;
        }

        public RedisStore(ConnectionMultiplexer connectionMultiplexer, int databaseNumber)
        {
            this.ConnectionMultiplexer = connectionMultiplexer;
            _databaseNumber = databaseNumber;
            _oneUse = false;
        }

        public override void Start()
        {
            base.Start();

            if (this.ConnectionMultiplexer == null)
            {
                this.ConnectionMultiplexer = ConnectionMultiplexer.Connect(_connections);
            }
            this.Database = this.ConnectionMultiplexer.GetDatabase(_databaseNumber);
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