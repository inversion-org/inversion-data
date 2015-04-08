using System.Collections.Generic;
using Inversion.Data;
using Harness.Example.Model;

namespace Harness.Example.Store
{
    public interface IUserStore : IStore
    {
        User Get(string username);
        IEnumerable<User> GetAll();
        void Put(User user);
        void Put(IEnumerable<User> users);
        void Delete(User user);
    }
}