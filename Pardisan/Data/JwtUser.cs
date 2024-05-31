using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Pardisan
{
    public class JwtUser
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

    }
}
