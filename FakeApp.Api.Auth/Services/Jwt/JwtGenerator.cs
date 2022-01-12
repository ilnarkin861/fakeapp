using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FakeApp.Domain.Entities;
using FakeApp.Domain.Enum;
using FakeApp.Infrastructure.Options;



namespace FakeApp.Api.Auth.Services.Jwt
{
    /// <summary>
    /// Класс, генерирующий jwt-токен
    /// </summary>
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IOptions<AuthOptions> _authOptions;

        
        public JwtGenerator(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions;
        }


        public string GenerateJwt(string identifier, UserLoginType loginType, User user)
        {
            var authParams = _authOptions.Value;
            
            var securityKey = authParams.GetSymmetricSecurityKey();

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, identifier),
                new("loginType", loginType == UserLoginType.Basic ? "Basic" : "Social")
            };
            
            var token = new JwtSecurityToken
            (
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}