using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace GuestControlApi.Auth
{
    public sealed class TokenBuilder
    {
        private readonly IConfiguration _configuration;

        public TokenBuilder(IConfiguration config)
        {
            this._configuration = config;
        }

        public string Build(PayloadJwt payload)
        {
            //adiciona os claims ao token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("subject", payload.sub),
                new Claim("name", payload.name),
                new Claim("subjectId", payload.subId.ToString())
            };

            //Adiciona os grupos
            claims.AddRange(payload.roles.Select(role => new Claim("roles", role)));

            SecurityKey chave = new SymmetricSecurityKey(TextEncodings.Base64Url.Decode(_configuration.GetSection("TokenConfigurations:Secret").Value));

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("TokenConfigurations:Issuer").Value,
                audience: _configuration.GetSection("TokenConfigurations:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(Convert.ToDouble(_configuration.GetSection("TokenConfigurations:Seconds").Value)),
                signingCredentials: new SigningCredentials(chave, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
