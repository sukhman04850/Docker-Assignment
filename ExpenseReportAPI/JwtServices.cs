using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseReportAPI
{
    public class JwtServices
    {
        public string SecretKey { get; set; }
        public int TokenDuration { get; set; }
        private readonly IConfiguration config;
        public JwtServices(IConfiguration _config)
        {
            config = _config;
            var secretKey  = config.GetSection("jwtConfig").GetSection("Key").Value;
            var duration = config.GetSection("jwtConfig").GetSection("Duration").Value;
            if(string.IsNullOrEmpty(secretKey)||string.IsNullOrEmpty(duration))
            {
                throw new Exception("Exception it is null here");
            }
            this.SecretKey = secretKey;
            this.TokenDuration = Int32.Parse(duration);
        }
        public string GenerateToken(String Id, String Name, String Email, Boolean IsAdmin)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            string role = "";
            if (IsAdmin)
            {
                role = "Admin";
            }
            var payload = new[]
            {
                new Claim("id",Id),
                new Claim("name",Name),
                new Claim("email",Email),
                new Claim("role",role),
             
            };
            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials: signature
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}

