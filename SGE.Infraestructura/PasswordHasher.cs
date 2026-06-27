using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    // Generar un Salt
    public static string GenerateSalt()
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(16); 
        return Convert.ToBase64String(saltBytes);
    }
    public static string ComputeHash(string contrasena)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(contrasena);

        // llamamos a el hash SHA-256
        byte[] hashBytes = SHA256.HashData(inputBytes);

        // Lo convertimos a string para guardarlo facilmente 
        return Convert.ToHexString(hashBytes);
    }

    // Verificar si la contraseña ingresada coincide con la guardada
    public static bool VerifyPassword(string contrasenaIngresada, string HashGuardado)
    {
        string newHash = ComputeHash(contrasenaIngresada);
        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(newHash), 
            Encoding.UTF8.GetBytes(HashGuardado)
        );
    }
}