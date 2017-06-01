using System;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;

namespace Inversion.Data
{
    public class AmazonSNSStore : StoreBase, IStoreHealth
    {
        protected readonly string TopicArn;
        protected readonly string Region;
        protected AmazonSimpleNotificationServiceClient Client;
        private readonly string _accessKey;
        private readonly string _accessSecret;
        private readonly bool _disableLogging;

        private bool _disposed;

        public AmazonSNSStore(string topicArn, string region, string accessKey, string accessSecret, bool disableLogging = false)
        {
            this.TopicArn = topicArn;
            this.Region = region;
            _accessKey = accessKey;
            _accessSecret = accessSecret;
            _disableLogging = disableLogging;
        }

        public override void Start()
        {
            base.Start();

            AWSCredentials credentials = new BasicAWSCredentials(_accessKey, _accessSecret);
            AmazonSimpleNotificationServiceConfig config = new AmazonSimpleNotificationServiceConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(this.Region),
                DisableLogging = _disableLogging,
                UseHttp = true
            };
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
