using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace Inversion.Data.Redis
{
    public class RedisStore : SyncBaseStore, IStoreHealth
    {
        private readonly string _connections;

        protected ConnectionMultiplexer ConnectionMultiplexer { get; private set; }

        protected IDatabase Database { get; private set; }

        private static readonly Dictionary<string, Tuple<ConnectionMultiplexer, int>> ConnectionMultiplexers =
            new Dictionary<string, Tuple<ConnectionMultiplexer, int>>();

        private readonly int _databaseNumber;

        private bool _disposed;

        public RedisStore(string connections, int databaseNumber)
        {
            _connections = connections;
            _databaseNumber = databaseNumber;
        }

        public ConnectionMultiplexer InhabitConnectionMultiplexer()
        {
            try
            {
                this.LockSlim.EnterWriteLock();

                if (!RedisStore.ConnectionMultiplexers.ContainsKey(_connections))
                {
                    RedisStore.ConnectionMultiplexers[_connections] =
                        new Tuple<ConnectionMultiplexer, int>(ConnectionMultiplexer.Connect(_connections), 1);
                }
                else
                {
                    Tuple<ConnectionMultiplexer, int> current = RedisStore.ConnectionMultiplexers[_connections];
                    RedisStore.ConnectionMultiplexers[_connections] =
                        new Tuple<ConnectionMultiplexer, int>(current.Item1, current.Item2 + 1);
                }
                return RedisStore.ConnectionMultiplexers[_connections].Item1;
            }
            finally
            {
                this.LockSlim.ExitWriteLock();
            }
        }

        public void AbandonConnectionMultiplexer()
        {
            try
            {
                this.LockSlim.EnterWriteLock();

                if (RedisStore.ConnectionMultiplexers.ContainsKey(_connections))
                {
                    Tuple<ConnectionMultiplexer, int> current = RedisStore.ConnectionMultiplexers[_connections];
                    RedisStore.ConnectionMultiplexers[_connections] =
                        new Tuple<ConnectionMultiplexer, int>(current.Item1, current.Item2 > 1 ? current.Item2 - 1 : 0);
                }
            }
            finally
            {
                this.LockSlim.ExitWriteLock();
            }
        }

        public override void Start()
        {
            base.Start();

            if (this.ConnectionMultiplexer == null)
            {
                this.ConnectionMultiplexer = this.InhabitConnectionMultiplexer();
            }
            this.Database = this.ConnectionMultiplexer.GetDatabase(_databaseNumber);
        }

        public override void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            this.AbandonConnectionMultiplexer();
        }

        public virtual bool GetHealth(out string result)
        {
            result = String.Empty;
            this.AssertIsStarted();

            TimeSpan ts = this.Database.Ping(CommandFlags.HighPriority);

            if (ts.TotalSeconds < 5)
            {
                return true;
            }

            result = String.Format("ping took {0} milliseconds.", ts.TotalMilliseconds);
            return false;
        }
    }
}