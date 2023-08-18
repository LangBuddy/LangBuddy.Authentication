using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBuddy.Authentication.Models.Dto
{
    public record AccountPasswordHashDto(
        byte[] PasswordSalt, byte[] PasswordHash
    );
}
