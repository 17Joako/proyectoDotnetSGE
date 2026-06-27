using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

public static class Jwt{//no estoy seguro de donde va esto

    public static string GenerarJwt(Usuario usuario)
    {
        
    var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, usuario.Id?.ToString() ?? ""),
    new Claim(ClaimTypes.Name, usuario.Nombre ?? ""),
    new Claim(ClaimTypes.Email, usuario.CorreoElectronico ?? ""),
    new Claim(ClaimTypes.Role, usuario.EsAdministrador ? "Administrador" : "Usuario")
};

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("EstaEsUnaClaveSuperSecretaParaJwtConMasDe32Caracteres123456"));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "mi-api",
            audience: "profesor",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    
     
}}