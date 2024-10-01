using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.WpfServies
{
    public class UserSession
    {
        private static UserSession _instance;

        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSession();
                }
                return _instance;
            }
        }

        public string JwtToken { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }

        private UserSession() { }
    }
}
