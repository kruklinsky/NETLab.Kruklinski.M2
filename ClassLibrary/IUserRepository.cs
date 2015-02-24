using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public interface IUserRepository
    {
        int CreateUser(string password);
    }
}
