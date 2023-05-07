using Application.DTOs.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Auth
{
    public class AuthToken
    {


        public string GenerateToken(UserDto userDto)
        {
          
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthInfo.keyString));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            // get roles from user.

            string[] Rols = userDto.UserRol.Select(c => (string)c.Rol.Name).ToArray();
            string RolText = string.Join(" ,",Rols);
            //Claims
            var Claims = new[]
            {
                new Claim(ClaimTypes.Name, userDto.Names),
                //new Claim(ClaimTypes.Email, userDto.Email),              
                new Claim("Id",userDto.Id.ToString()),
                new Claim("UserRols",RolText)

            };

            List<Claim> ClaimList = new List<Claim>() { };
          

            foreach (var Rol in userDto.UserRol)
            {
                var x = new Claim(ClaimTypes.Role,Rol.Rol.Name);
                ClaimList.Add(x);
            }
            ClaimList.AddRange(Claims);
            //payload

            var payload = new JwtPayload(
                 AuthInfo.IssuerString,
                 AuthInfo.AudienceString,
                 ClaimList,
                 DateTime.Now,
                 DateTime.UtcNow.AddHours(36)

            )
            { 
                
            };
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
