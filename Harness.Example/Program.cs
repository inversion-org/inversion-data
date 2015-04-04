using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harness.Example.Model;

namespace Harness.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            User originalUser = new User(
                "fractos", "1234",
                new UserMetadata(
                    new Dictionary<string, string>
                    {
                        {"type", "normal"}
                    }));

            User changedUser = originalUser.Mutate(b =>
            {
                b.Password = "1234";
                b.Metadata.Metadata.Add("newsletter", "true");
                return b;
            });


        }
    }
}
