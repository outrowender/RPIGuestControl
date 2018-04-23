using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace GuestControlApi.Auth
{
    public class JwtModels
    {
        public static SymmetricSecurityKey Create(string secret) => new SymmetricSecurityKey(TextEncodings.Base64Url.Decode(secret));
    }

    public class PayloadJwt
    {
        public string sub { get; set; }
        public string subId { get; set; }
        public string name { get; set; }
        public List<string> roles { get; set; }
    }
}
