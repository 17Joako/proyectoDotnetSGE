using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher: IPasswordHasher
{
    // Generar un Salt
    public string Hash(string contrasena)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(contrasena);

        // llamamos a el hash SHA-256
        byte[] hashBytes = SHA256.HashData(inputBytes);

        // Lo convertimos a string para guardarlo facilmente 
        return Convert.ToHexString(hashBytes);
    }

    // Verificar si la contraseña ingresada coincide con la guardada
    public bool Verify(string contrasenaIngresada, string HashGuardado)
    {
        string newHash = Hash(contrasenaIngresada);
        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(newHash), 
            Encoding.UTF8.GetBytes(HashGuardado)
        );
    }
}