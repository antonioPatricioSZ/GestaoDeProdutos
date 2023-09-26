using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Token;

public class TokenController {

    private readonly double _tempoVidaToken;
    private readonly string _chaveToken;

    public TokenController(double tempoVidaToken, string chaveToken) {
        _tempoVidaToken = tempoVidaToken;
        _chaveToken = chaveToken;
    }

    public string GenerateToken(long userId, List<string> userRoles) {

        var handler = new JwtSecurityTokenHandler();       

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = GenerateClaims(userId, userRoles),
            SigningCredentials = Credentials(),
            Expires = DateTime.UtcNow.AddMinutes(_tempoVidaToken),
        
        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(long userId, List<string> userRoles) {

        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));

        foreach (var role in userRoles) {
            ci.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return ci;
        
    }

    private SigningCredentials Credentials() {
        var key = Encoding.ASCII.GetBytes(_chaveToken);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256
        );
        return credentials;
    }
    

}
