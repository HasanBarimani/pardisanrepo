using Pardisan.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pardisan.Interfaces
{
    public interface IJwtManager
    {
        string CreateToken(ApplicationUser user);
        ApplicationUser GetUser();
    }
}
