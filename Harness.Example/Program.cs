using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harness.Example.Model;
using Harness.Example.Store;

namespace Harness.Example
{
    class Program
    {
        const string DictionaryFilename = "..\\..\\words.txt";

        const string ConnectionString = "mongodb://localhost";
        const string DatabaseName = "test";
        const string CollectionName = "users";

        static List<User> Users;

        static List<string> Words;

        static readonly Random Random = new Random();

        static void Main(string[] args)
        {
            Words = LoadDictionary(DictionaryFilename);

            //PopulateUsers(100);          

            Users = ReadUsers().ToList();

            UpdateUsers();

            DeleteUsers();

            Console.WriteLine("user count = {0}", Users.Count);
            Console.ReadLine();
        }

        static void PopulateUsers(int total)
        {
            Users = CreateUsers(total);

            SaveUsers();
        }

        static List<string> LoadDictionary(string path)
        {
            return File.ReadAllLines(path).ToList();
        }

        static List<User> CreateUsers(int total)
        {
            List<string> tlds = new List<string> {"com", "net", "co.uk", "me"};

            List<User> users = new List<User>();

            for (int x = 0; x < total; x++)
            {
                users.Add(new User.Builder
                {
                    Username = String.Format("{0}@{1}.{2}", Words[Random.Next(Words.Count)], Words[Random.Next(Words.Count)], tlds[Random.Next(tlds.Count)]),
                    Password = Words[Random.Next(Words.Count)],
                    Metadata = new UserMetadata.Builder
                    {
                        Metadata = new Dictionary<string, string>
                        {
                            { Words[Random.Next(Words.Count)], Words[Random.Next(Words.Count)] }
                        }
                    }
                });
            }

            return users;
        }

        static void SaveUsers()
        {
            using (IUserStore userStore = new MongoDBUserStore(ConnectionString, DatabaseName, CollectionName))
            {
                userStore.Start();

                userStore.Put(Users);
            }
        }

        static IEnumerable<User> ReadUsers()
        {
            using (IUserStore userStore = new MongoDBUserStore(ConnectionString, DatabaseName, CollectionName))
            {
                userStore.Start();

                return userStore.GetAll();
            }
        }

        static void UpdateUsers()
        {
            using (IUserStore userStore = new MongoDBUserStore(ConnectionString, DatabaseName, CollectionName))
            {
                userStore.Start();

                foreach (User user in Users)
                {
                    User updatedUser = user.Mutate(b =>
                    {
                        b.Password = Words[Random.Next(Words.Count)];
                        return b;
                    });

                    userStore.Put(updatedUser);
                }
            }
        }

        static void DeleteUsers()
        {
            using (IUserStore userStore = new MongoDBUserStore(ConnectionString, DatabaseName, CollectionName))
            {
                userStore.Start();

                foreach (User user in Users)
                {
                    userStore.Delete(user);
                }
            }
        }
    }
}