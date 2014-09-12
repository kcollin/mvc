using System;
using System.Linq;

namespace Identity_test_1.Models
{
    interface IUserRepository
    {
        MyUser GetUser(int id);
        MyUser GetUser(string username);
        String GetUserName(MyUser user);
        IQueryable<MyUser> visAlleBrukere();
    }
}
