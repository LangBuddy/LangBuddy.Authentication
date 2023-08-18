using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBuddy.Authentication.Service.Authentication.Common
{
    public interface ICreatePasswordHashCommand
    {
        void Invoke(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
