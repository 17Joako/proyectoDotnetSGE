using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

public static class Jwt{//no estoy seguro de donde va esto

    public static string GenerarJwt(Usuario usuario)
    {
      var claims = new List<Claim>
        {
            new Claim("idUsuario", usuario.Id?.ToString() ?? ""),
            new Claim("nombre", usuario.Nombre ?? ""),
            new Claim("correo", usuario.CorreoElectronico ?? ""),
            new Claim("esAdmin", usuario.EsAdministrador.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("Esta-es-La-Clave-mas-secreta-123456"));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "mi-api",
            audience: "profesor",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),//esto tengo que preguntar
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}