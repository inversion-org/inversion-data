using System;
using System.Collections.Generic;
using System.Threading;
using StackExchange.Redis;

namespace Inversion.Data.Store
{
    public class RedisStore : StoreBase, IStoreHealth
    {
        private readonly string _connections;

        protected ConnectionMultiplexer ConnectionMultiplexer { get; set; }

        protected IDatabase Database { get; private set; }

        private static readonly Dictionary<string, Tuple<ConnectionMultiplexer, int>> ConnectionMultiplexers =
            new Dictionary<string, Tuple<ConnectionMultiplexer, int>>();

        private static readonly ReaderWriterLockSlim _lockSlim = new ReaderWriterLockSlim();

        private readonly int _databaseNumber;

        private bool _disposed;

        public RedisStore(string connections, int databaseNumber)
        {
            _connections = connections;
            _databaseNumber = databaseNumber;
        }

        private ConnectionMultiplexer InhabitConnectionMultiplexer()
        {
            ConfigurationOptions options = this.GetConfigurationOptions();

            try
            {
                _lockSlim.EnterWriteLock();

                if (!RedisStore.ConnectionMultiplexers.ContainsKey(_connections))
                {
                    StackExchange.Redis.ConnectionMultiplexer connectionMultiplexer =
                        StackExchange.Redis.ConnectionMultiplexer.Connect(options);

                    RedisStore.ConnectionMultiplexers[_connections] =
                        new Tuple<ConnectionMultiplexer, int>(connectionMultiplexer, 1);
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
                _lockSlim.ExitWriteLock();
            }
        }

        public void AbandonConnectionMultiplexer()
        {
            try
            {
                _lockSlim.EnterWriteLock();

                if (RedisStore.ConnectionMultiplexers.ContainsKey(_connections))
                {
                    Tuple<ConnectionMultiplexer, int> current = RedisStore.ConnectionMultiplexers[_connections];
                    RedisStore.ConnectionMultiplexers[_connections] =
                        new Tuple<ConnectionMultiplexer, int>(current.Item1, current.Item2 > 1 ? current.Item2 - 1 : 0);
                }
            }
            finally
            {
                _lockSlim.ExitWriteLock();
            }
        }

        protected virtual ConfigurationOptions GetConfigurationOptions()
        {
            return ConfigurationOptions.Parse(_connections);
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