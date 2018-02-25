using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace Inversion.Data
{
    public class DynamoDBStore : StoreBase, IStoreHealth
    {
        private readonly string _serviceUrl;
        protected AmazonDynamoDBClient Client;
        protected DynamoDBContext Context;
        private readonly string _accessKey;
        private readonly string _accessSecret;

        private bool _disposed;

        public DynamoDBStore(string serviceUrl, string accessKey, string accessSecret)
        {
            _serviceUrl = serviceUrl;
            _accessKey = accessKey;
            _accessSecret = accessSecret;
        }

        public override void Start()
        {
            base.Start();

            AWSCredentials credentials = new BasicAWSCredentials(_accessKey, _accessSecret);
            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig { ServiceURL = _serviceUrl };
            this.Client = new AmazonDynamoDBClient(credentials, config);
            this.Context = new DynamoDBContext(this.Client);
        }

        public sealed override void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            if (this.HasStarted)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                }
                if (this.Client != null)
                {
                    this.Client.Dispose();
                }
            }
        }

        public virtual bool GetHealth(out string result)
        {
            this.AssertIsStarted();

            result = String.Empty;

            ListTablesResponse response = this.Client.ListTablesAsync(new ListTablesRequest {
                Limit = 1
            }).Result;

            if (response.TableNames.Count > 0)
            {
                return true;
            }

            result = "No tables could be listed.";
            return false;
        }
    }
}