using System;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;

namespace Inversion.Data
{
    public class AmazonSNSStore : StoreBase, IStoreHealth
    {
        protected readonly string TopicArn;
        protected readonly string Region;
        protected readonly string ServiceURL;
        protected AmazonSimpleNotificationServiceClient Client;
        private readonly string _accessKey;
        private readonly string _accessSecret;

        private bool _disposed;

        public AmazonSNSStore(string topicArn, string region, string accessKey="", string accessSecret="", string serviceURL="")
        {
            this.TopicArn = topicArn;
            this.Region = region;
            _accessKey = accessKey;
            _accessSecret = accessSecret;
            this.ServiceURL = serviceURL;
        }

        public override void Start()
        {
            base.Start();

            AWSCredentials credentials = null;
            if (!String.IsNullOrEmpty(_accessKey) && !String.IsNullOrEmpty(_accessSecret))
            {
                credentials = new BasicAWSCredentials(_accessKey, _accessSecret);
            }
            else
            {
                credentials = new Amazon.Runtime.InstanceProfileAWSCredentials();
            }

            AmazonSimpleNotificationServiceConfig config = new AmazonSimpleNotificationServiceConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(this.Region),
            };
            if (!String.IsNullOrEmpty(this.ServiceURL))
            {
                config.ServiceURL = this.ServiceURL;
            }
            this.Client = new AmazonSimpleNotificationServiceClient(credentials, config);
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
