using System;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Inversion.Data
{
    public class AmazonSQSStore : StoreBase, IStoreHealth
    {
        protected readonly string ServiceUrl;
        protected readonly string Region;
        protected AmazonSQSClient Client;
        private readonly string _accessKey;
        private readonly string _accessSecret;
        private readonly AWSCredentials _credentials;

        private bool _disposed;

        public AmazonSQSStore(string serviceUrl, string region, string accessKey="", string accessSecret="", AWSCredentials credentials = null)
        {
            this.ServiceUrl = serviceUrl;
            this.Region = region;
            _accessKey = accessKey;
            _accessSecret = accessSecret;
            _credentials = credentials;
        }

        public override void Start()
        {
            base.Start();

            AWSCredentials credentials = null;
            if (_credentials != null)
            {
                credentials = _credentials;
            }
            else if (!String.IsNullOrEmpty(_accessKey) && !String.IsNullOrEmpty(_accessSecret))
            {
                credentials = new BasicAWSCredentials(_accessKey, _accessSecret);
            }
            else
            {
                credentials = new Amazon.Runtime.InstanceProfileAWSCredentials();
            }

            AmazonSQSConfig config = new AmazonSQSConfig
            {
                ServiceURL = this.ServiceUrl,
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(this.Region)
            };
            this.Client = new AmazonSQSClient(credentials, config);
        }

        public sealed override void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            if (this.HasStarted)
            {
                if (this.Client != null)
                {
                    this.Client.Dispose();
                }
            }
        }

        public bool GetHealth(out string result)
        {
            this.AssertIsStarted();

            result = "not supported";

            return true;
        }
    }
}
