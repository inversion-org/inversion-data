using System;
using System.Xml;
using Newtonsoft.Json.Linq;

using Inversion;

namespace Harness.Example.Model
{
    public class User : IData
    {
        private readonly string _id;
        private readonly string _username;
        private readonly string _password;
        private readonly UserMetadata _metadata;

        public string ID { get { return _id; } }
        public string Username { get { return _username; } }
        public string Password { get { return _password; } }

        public UserMetadata Metadata { get { return _metadata; } }

        public User(User user)
        {
            this._id = user.ID;
            this._username = user.Username;
            this._password = user.Password;
            this._metadata = new UserMetadata(user.Metadata);
        }

        public User(string id, string username, string password, UserMetadata metadata)
        {
            this._id = id;
            this._username = username;
            this._password = password;
            this._metadata = metadata;
        }

        public User Mutate(Func<Builder, User> mutator)
        {
            Builder builder = new Builder(this);
            return mutator(builder);
        }

        public class Builder
        {
            public string ID { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }

            public UserMetadata.Builder Metadata { get; set; }

            public Builder()
            {
                this.Metadata = new UserMetadata.Builder();
                this.ID = Guid.NewGuid().ToString();
            }

            public Builder(User user)
            {
                this.ID = user.ID;
                this.Username = user.Username;
                this.Password = user.Password;
                this.Metadata = user.Metadata;
            }

            public Builder(string id, string username, string password, UserMetadata metadata)
            {
                this.ID = id;
                this.Username = username;
                this.Password = password;
                this.Metadata = new UserMetadata(metadata);
            }

            public User ToModel()
            {
                return new User(this.ID, this.Username, this.Password, this.Metadata);
            }

            public Builder FromModel(User user)
            {
                this.ID = user.ID;
                this.Username = user.Username;
                this.Password = user.Password;
                this.Metadata = new UserMetadata(user.Metadata);
                return this;
            }

            public static implicit operator User(Builder builder)
            {
                return builder.ToModel();
            }

            public static implicit operator Builder(User model)
            {
                return new Builder(model);
            }

        }

        public object Clone()
        {
            return new User(this);
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("user");
            
            writer.WriteElementString("id", this.ID);
            writer.WriteElementString("username", this.Username);
            writer.WriteElementString("password", this.Password);
            
            this.Metadata.ToXml(writer);

            writer.WriteEndElement();
        }

        public Newtonsoft.Json.Linq.JObject Data
        {
            get
            {
                JObject j = new JObject
                {
                    {"id", this.ID},
                    {"username", this.Username},
                    {"password", this.Password},
                    {"metadata", ((IData) (this.Metadata)).Data}
                };
                return j;
            }
        }

        public void ToJson(Newtonsoft.Json.JsonWriter writer)
        {
            writer.WriteStartObject();
            
            writer.WritePropertyName("id");
            writer.WriteValue(this.ID);
            writer.WritePropertyName("username");
            writer.WriteValue(this.Username);
            writer.WritePropertyName("password");
            writer.WriteValue(this.Password);

            this.Metadata.ToJson(writer);

            writer.WriteEndObject();
        }
    }
}