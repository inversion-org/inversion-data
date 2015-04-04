using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Inversion;
using Newtonsoft.Json.Linq;

namespace Harness.Example.Model
{
    public class UserMetadata : IData
    {
        private readonly ImmutableDictionary<string, string> _metadata;

        public ImmutableDictionary<string, string> Metadata { get { return _metadata; } }

        public UserMetadata(UserMetadata usermetadata)
        {
            ImmutableDictionary<string, string>.Builder b = ImmutableDictionary.CreateBuilder<string, string>();
            b.AddRange(usermetadata.Metadata);
            this._metadata = b.ToImmutable();
        }

        public UserMetadata(IDictionary<string, string> metadata)
        {
            ImmutableDictionary<string, string>.Builder b = ImmutableDictionary.CreateBuilder<string, string>();
            b.AddRange(metadata);
            this._metadata = b.ToImmutable();
        }

        public class Builder
        {
            public Dictionary<string, string> Metadata { get; set; }

            public Builder()
            {
                this.Metadata = new Dictionary<string, string>();
            }

            public Builder(UserMetadata usermetadata)
            {
                this.Metadata = new Dictionary<string, string>(usermetadata.Metadata);
            }

            public Builder(IDictionary<string, string> metadata)
            {
                this.Metadata = new Dictionary<string, string>(metadata);
            }

            public UserMetadata ToModel()
            {
                return new UserMetadata(this.Metadata);
            }

            public Builder FromModel(UserMetadata usermetadata)
            {
                this.Metadata = new Dictionary<string, string>(usermetadata.Metadata);
                return this;
            }

            public static implicit operator UserMetadata(Builder builder)
            {
                return builder.ToModel();
            }

            public static implicit operator Builder(UserMetadata model)
            {
                return new Builder(model);
            }

            public UserMetadata Mutate(Func<Builder, UserMetadata> mutator)
            {
                Builder builder = new Builder(this);
                return mutator(builder);
            }
        }

        public object Clone()
        {
            return new UserMetadata(this);
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("usermetadata");
            
            foreach (KeyValuePair<string, string> kvp in this.Metadata)
            {
                writer.WriteElementString(kvp.Key, kvp.Value);
            }

            writer.WriteEndElement();
        }

        Newtonsoft.Json.Linq.JObject IData.Data
        {
            get
            {
                return JObject.FromObject(this.Metadata);
            }
        }

        public void ToJson(Newtonsoft.Json.JsonWriter writer)
        {
            writer.WriteStartObject();

            foreach (KeyValuePair<string, string> kvp in this.Metadata)
            {
                writer.WritePropertyName(kvp.Key);
                writer.WriteValue(kvp.Value);
            }

            writer.WriteEndObject();
        }
    }
}