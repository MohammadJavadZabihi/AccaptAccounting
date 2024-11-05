using Accapt.Core.Servies.InterFace;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class JwtHelper : IJwtHelper
    {
        JwtSecurityTokenHandler tokenHandeler = new JwtSecurityTokenHandler();

        public string GetUserIdFromToken(string token)
        {
            var readToken = tokenHandeler.ReadJwtToken(token);
            var claim = readToken.Claims.FirstOrDefault(c => c.Type == "userId").Value;

            return claim;
        }

        public string GetUserNameFromToken(string token)
        {
            var readToken = tokenHandeler.ReadJwtToken(token);
            var claim = readToken.Claims.FirstOrDefault(c => c.Type == "userName").Value;

            return claim;
        }
    }
}
