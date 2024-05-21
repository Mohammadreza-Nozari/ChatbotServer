using AIChatbotServices.Database.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AIChatbotServices.Logics;
public class AuthenticationLogic
{
    readonly IConfiguration _configuration;
    public AuthenticationLogic(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJSONWebToken(UserEntity user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
          _configuration["Jwt:Issuer"],
          new List<Claim>()
          {
              new Claim(nameof(user.Id),user.Id.ToString()),
              new Claim(nameof(user.UserName),user.UserName),
              new Claim(nameof(user.TenantId),user.TenantId.ToString()),
          },
          expires: DateTime.Now.AddMonths(1),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
