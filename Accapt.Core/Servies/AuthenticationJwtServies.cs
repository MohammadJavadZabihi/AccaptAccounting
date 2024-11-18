using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class AuthenticationJwtServies : IAuthenticationJwtServies
    {
        private readonly IConfiguration _configuration;
        public AuthenticationJwtServies(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
        }

        public async Task<string> AuthenticatJwtToken(string userId, string userName)
        {
            var security = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("userName", userName));
            claimsForToken.Add(new Claim("userId", userId));

            var jwtSecurityToke = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.Now,
                DateTime.Now.AddHours(24),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToke);

            return tokenToReturn;
        }

        public async Task<string> AuthenticatJwtTokenForMobileApp(LoginProviderServiceDTO loginProviderServiceDTO)
        {
            var security = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var singingCredentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);
            var clamsForToken = new List<Claim>();
            clamsForToken.Add(new Claim("UserName", loginProviderServiceDTO.UserName));
            clamsForToken.Add(new Claim("userId", loginProviderServiceDTO.UserId));
            clamsForToken.Add(new Claim("ServiceProviderId", loginProviderServiceDTO.ServiceProviderId.ToString()));

            var jwtSevurityToken = new JwtSecurityToken(
               _configuration["Authentication:Issuer"],
               _configuration["Authentication:Audience"],
               clamsForToken,
               DateTime.Now,
               DateTime.Now.AddDays(1),
               singingCredentials);

            var tokenReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSevurityToken);

            return tokenReturn;
        }
    }
}
