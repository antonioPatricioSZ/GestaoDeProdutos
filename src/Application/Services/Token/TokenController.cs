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
        ci.AddClaim(new Claim("Id", userId.ToString()));

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


    public ClaimsPrincipal ValidateToken(string token) {

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_chaveToken); 

        var parametrosValidacao = new TokenValidationParameters {
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        var claims = tokenHandler.ValidateToken(token, parametrosValidacao, out _);
        return claims;

    }


    public long UserIdInToken(string token) {

        var claims = ValidateToken(token);
        var idClaim = claims.FindFirst("Id");

        if (idClaim != null) {
            var valor = long.Parse(idClaim.Value);
            return valor;      
        } else {
            return 0;
        }

    }


}
