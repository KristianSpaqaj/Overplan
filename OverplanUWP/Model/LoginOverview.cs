using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.Model
{
    public class LoginOverview
    {
        public LoginOverview(string username, string password, bool leader)
        {
            Username = username;
            Password = password;
            Leader = leader;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Leader { get; set; }
    }
}
